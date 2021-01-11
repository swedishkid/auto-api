using System.Collections.Generic;
using AutoApi.SourceGenerator.CodeGeneration.Api;
using AutoApi.SourceGenerator.Definition;

namespace AutoApi.SourceGenerator.CodeGeneration
{
    public class CodeGenerationManager
    {
        private readonly IDefinition _definition;

        public CodeGenerationManager(IDefinition definition)
        {
            _definition = definition;
        }

        public IEnumerable<CodeFile> GenerateCode()
        {
            IEnumerable<ICodeGenerator> codeGenerators = new List<ICodeGenerator>()
            {
                new ApiCodeGenerator(),
                new EntityCodeGenerator(),
                new OpenApiClientGenerator(),
                new WsdlServiceClientGenerator(),
                new AutoInterfaceCodeGenerator(),
                new TypedOptionsCodeGenerator()
            };

            var files = new List<CodeFile>();

            foreach (var generator in codeGenerators)
            {
                var codeGenerationContext = new CodeGenerationContext(_definition);
                generator.GenerateCode(codeGenerationContext);
                files.AddRange(codeGenerationContext.Files);
            }
            
            return files;
        }
    }
}