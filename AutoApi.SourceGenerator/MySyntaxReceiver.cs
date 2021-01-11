using System.Collections.Generic;
using System.Linq;
using AutoApi.SourceGenerator.Definition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoApi.SourceGenerator
{
    public class MySyntaxReceiver : ISyntaxReceiver, IDefinition
    {
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            var _ = syntaxNode switch
            {
                ClassDeclarationSyntax classDeclarationSyntax => ProcessClass(classDeclarationSyntax),
                InterfaceDeclarationSyntax interfaceDeclarationSyntax => ProcessInterface(
                    interfaceDeclarationSyntax),
                _ => true
            };
        }

        private bool ProcessInterface(InterfaceDeclarationSyntax syntax)
        {
            var interfaceName = syntax.Identifier.ValueText;
            var interfaceDefinition = new InterfaceDefinition(interfaceName);
            interfaceDefinition.Attributes.AddRange(GetAttributes(syntax));
            interfaceDefinition.Properties.AddRange(GetProperties(syntax));
            Interfaces.Add(interfaceDefinition);
            return true;
        }

        public List<ClassDefinition> Classes { get; } = new();

        public List<InterfaceDefinition> Interfaces { get; } = new();

        private bool ProcessClass(ClassDeclarationSyntax syntax)
        {
            var className = syntax.Identifier.ValueText;
            string namespaceName = null;

            if (SyntaxNodeHelper.TryGetParentSyntax<NamespaceDeclarationSyntax>(syntax,
                out var namespaceDeclarationSyntax))
            {
                namespaceName = namespaceDeclarationSyntax.Name.ToString();
            }

            var classDefinition = new ClassDefinition(className, namespaceName);


            classDefinition.Constructors.AddRange(GetConstructors(syntax));
            classDefinition.Attributes.AddRange(GetAttributes(syntax));
            classDefinition.Properties.AddRange(GetProperties(syntax));
            classDefinition.Fields.AddRange(GetFields(syntax));
            classDefinition.Methods.AddRange(GetMethods(syntax));
            Classes.Add(classDefinition);
            return true;
        }

        private static IEnumerable<ConstructorDefinition> GetConstructors(ClassDeclarationSyntax syntax)
        {
            var constructors = syntax.Members
                .Where(x => x.IsKind(SyntaxKind.ConstructorDeclaration))
                .Cast<ConstructorDeclarationSyntax>();

            foreach (var constructor in constructors)
            {
                yield return GetConstructorDefinition(constructor);
            }
        }

        private static ConstructorDefinition GetConstructorDefinition(ConstructorDeclarationSyntax constructor)
        {
            var definition = new ConstructorDefinition
            {
                Name = constructor.Identifier.NormalizeWhitespace().ToFullString()
            };

            definition.Attributes.AddRange(GetAttributes(constructor));
            definition.Parameters.AddRange(GetParameters(constructor));

            return definition;
        }


        private static IEnumerable<ParameterDefinition> GetParameters(BaseMethodDeclarationSyntax syntax)
        {
            var parameters = syntax.ParameterList.Parameters;

            foreach (var parameter in parameters)
            {
                yield return GetParameterDefinition(syntax, parameter);
            }
        }

        private static ParameterDefinition GetParameterDefinition(BaseMethodDeclarationSyntax syntax,
            ParameterSyntax parameter)
        {
            var parameterName = parameter.Identifier.ToFullString();
            var parameterType = parameter.Type.NormalizeWhitespace().ToFullString();
            var definition = new ParameterDefinition(parameterName, parameterType);
            definition.Attributes.AddRange(GetAttributes(syntax));
            return definition;
        }

        private static IEnumerable<MethodDefinition> GetMethods(ClassDeclarationSyntax syntax)
        {
            var methods = syntax.Members
                .Where(x => x.IsKind(SyntaxKind.MethodDeclaration))
                .Cast<MethodDeclarationSyntax>();

            foreach (var method in methods)
            {
                yield return GetMethodDefinition(method);
            }
        }

        private static MethodDefinition GetMethodDefinition(MethodDeclarationSyntax method)
        {
            var name = method.Identifier.NormalizeWhitespace().ToFullString();
            var returnType = method.ReturnType.NormalizeWhitespace().ToFullString();

            var methodDefinition = new MethodDefinition
            {
                MethodName = name,
                ReturnTypeString = returnType
            };
            
            methodDefinition.Attributes.AddRange(GetAttributes(method));
            methodDefinition.Parameters.AddRange(GetParameters(method));
            return methodDefinition;
        }

        private static IEnumerable<FieldDefinition> GetFields(TypeDeclarationSyntax syntax)
        {
            var fields = syntax.Members.Where(x
                    => x.IsKind(SyntaxKind.FieldDeclaration))
                .Cast<FieldDeclarationSyntax>();

            foreach (var field in fields)
            {
                var variables = field.Declaration.Variables;

                foreach (var variable in variables)
                {
                    yield return GetFieldDefinition(field, variable);
                }
            }
        }

        private static FieldDefinition GetFieldDefinition(FieldDeclarationSyntax field, VariableDeclaratorSyntax variable)
        {
            var fieldName = variable.Identifier.NormalizeWhitespace().ToFullString();
            var fieldType = field.Declaration.Type.NormalizeWhitespace().ToFullString();
            var fieldDefinition = new FieldDefinition(fieldName, fieldType);
            fieldDefinition.Attributes.AddRange(GetAttributes(field));
            return fieldDefinition;
        }

        private static IEnumerable<PropertyDefinition> GetProperties(TypeDeclarationSyntax syntax)
        {
            var properties =
                syntax.Members
                    .Where(x => x.IsKind(SyntaxKind.PropertyDeclaration))
                    .Cast<PropertyDeclarationSyntax>();

            foreach (var property in properties)
            {
                yield return GetPropertyDefinition(property);
            }
        }

        private static PropertyDefinition GetPropertyDefinition(PropertyDeclarationSyntax property)
        {
            var propertyName = property.Identifier.NormalizeWhitespace().ToFullString();
            var propertyType = property.Type.NormalizeWhitespace().ToFullString();
            var propertyDefinition = new PropertyDefinition(propertyName, propertyType);
            propertyDefinition.Attributes.AddRange(GetAttributes(property));
            return propertyDefinition;
        }
        
        private static IEnumerable<AttributeDefinition> GetAttributes(MemberDeclarationSyntax syntax)
        {
            var attributes = syntax.AttributeLists.SelectMany(x => x.Attributes);

            foreach (var attributeSyntax in attributes)
            {
                var attributeName = attributeSyntax.Name.NormalizeWhitespace().ToFullString();
                var arguments = attributeSyntax.ArgumentList?.Arguments.ToList();

                var attributeDefinition = new AttributeDefinition(attributeName);

                if (arguments != null)
                {
                    foreach (var argument in arguments)
                    {
                        var argumentExpression = argument.Expression.NormalizeWhitespace().ToFullString();
                        if (argument.NameEquals == null)
                        {
                            attributeDefinition.Arguments.Add(new ArgumentDefinition(argumentExpression));
                        }
                        else
                        {
                            var argumentName = argument.NameEquals.Name.Identifier.ValueText;
                            attributeDefinition.Arguments.Add(new ArgumentDefinition(argumentName, argumentExpression));
                        }
                    }
                }

                yield return attributeDefinition;
            }
        }
    }
}