using System;
using System.Collections.Generic;
using System.Linq;
using AutoApi.SourceGenerator.Definition;

namespace AutoApi.SourceGenerator.CodeGeneration.Api
{
    public class ApiCodeGenerator : ICodeGenerator
    {
        public void GenerateCode(CodeGenerationContext context)
        {
            var classes = context.Definition.Classes;
            var candidateClasses = GetCandidateClasses(classes)
                .Distinct();

            var template = new ApiHandlerTemplate
            {
                Classes = candidateClasses
            };

            var output = template.TransformText();
            context.Files.Add(new CodeFile()
            {
                HintName = "ApiRouter.cs",
                SourceCode = output
            });
        }

        public static bool HasOneOfAttributes(IEnumerable<AttributeDefinition> attributes)
        {            
            var attributeNames = GetAttributeNames();
            return attributes
                .Select(attribute => 
                    attributeNames.Contains(attribute.AttributeName, StringComparer.OrdinalIgnoreCase))
                .FirstOrDefault();
        }
        
        internal static IEnumerable<ClassDefinition> GetCandidateClasses(IEnumerable<ClassDefinition> classes)
        {
            foreach (var @class in classes)
            {
                // see if class contains any route or http attributes.
                if (HasOneOfAttributes(@class.Attributes))
                {
                    yield return @class;
                }

                // see if any method level attribute matches any http based attributes.
                var methods = @class.Methods;

                foreach (var method in methods.Where(method => HasOneOfAttributes(method.Attributes)))
                {
                    yield return @class;
                }
            }
        }

        internal static IEnumerable<string> GetAttributeNames()
        {
            yield return nameof(HttpGetAttribute);
            yield return nameof(HttpPostAttribute);
            yield return nameof(HttpPatchAttribute);
            yield return nameof(HttpPutAttribute);
            yield return nameof(HttpDeleteAttribute);
            yield return nameof(RouteAttribute);

            // add attribute without postfix 'Attribute'
            yield return RemoveAttributePrefix(nameof(HttpGetAttribute));
            yield return RemoveAttributePrefix(nameof(HttpPostAttribute));
            yield return RemoveAttributePrefix(nameof(HttpPatchAttribute));
            yield return RemoveAttributePrefix(nameof(HttpPutAttribute));
            yield return RemoveAttributePrefix(nameof(HttpDeleteAttribute));
            yield return RemoveAttributePrefix(nameof(RouteAttribute));
        }

        private static string RemoveAttributePrefix(string attributeName)
        {
            return attributeName.Replace("Attribute", string.Empty);
        }
    }
}