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
            var classDefinition = new ClassDefinition(className);
            classDefinition.Attributes.AddRange(GetAttributes(syntax));
            classDefinition.Properties.AddRange(GetProperties(syntax));
            classDefinition.Fields.AddRange(GetFields(syntax));
            Classes.Add(classDefinition);
            return true;
        }

        private IEnumerable<FieldDefinition> GetFields(TypeDeclarationSyntax syntax)
        {
            var fields = syntax.Members.Where(x
                    => x.IsKind(SyntaxKind.FieldDeclaration))
                .Cast<FieldDeclarationSyntax>();

            foreach (var field in fields)
            {
                var variables = field.Declaration.Variables;

                foreach (var variable in variables)
                {
                    var fieldName = variable.Identifier.NormalizeWhitespace().ToFullString();
                    var fieldType = field.Declaration.Type.NormalizeWhitespace().ToFullString();
                    var fieldDefinition = new FieldDefinition(fieldName, fieldType);
                    fieldDefinition.Attributes.AddRange(GetAttributes(field));
                    yield return fieldDefinition;
                }
            }
        }

        private IEnumerable<PropertyDefinition> GetProperties(TypeDeclarationSyntax syntax)
        {
            var properties =
                syntax.Members
                    .Where(x => x.IsKind(SyntaxKind.PropertyDeclaration))
                    .Cast<PropertyDeclarationSyntax>();

            foreach (var property in properties)
            {
                var propertyName = property.Identifier.NormalizeWhitespace().ToFullString();
                var propertyType = property.Type.NormalizeWhitespace().ToFullString();
                var propertyDefinition = new PropertyDefinition(propertyName, propertyType);
                propertyDefinition.Attributes.AddRange(GetAttributes(syntax));
                yield return propertyDefinition;
            }
        }

        private IEnumerable<AttributeDefinition> GetAttributes(MemberDeclarationSyntax syntax)
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