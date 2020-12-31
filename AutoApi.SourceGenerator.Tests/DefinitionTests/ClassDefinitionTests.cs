using System;
using AutoApi.SourceGenerator.Definition;
using Xunit;
using static Xunit.Assert;
namespace AutoApi.SourceGenerator.Tests.DefinitionTests
{
    public class ClassDefinitionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptEmptyOrWhitespaceClassName(string className)
        {
            var exception = Throws<ArgumentException>(() =>
            {
                new ClassDefinition(className);
            });
            
            Equal("Value cannot be null or whitespace. (Parameter 'className')" , exception.Message);
            Equal("className", exception.ParamName);
        }

        [Fact]
        public void WillSetValidClassNameAsProperty()
        {
            ClassDefinition definition = new("Custom");
            Equal("Custom", definition.ClassName);
        }
        
        [Fact]
        public void AttributeCollectionIsSet()
        {
            ClassDefinition definition = new("Custom");
            NotNull(definition.Attributes);
        }
        
        [Fact]
        public void PropertiesCollectionIsSet()
        {
            ClassDefinition definition = new("Custom");
            NotNull(definition.Properties);
        }
        
        [Fact]
        public void FieldsCollectionIsSet()
        {
            ClassDefinition definition = new("Custom");
            NotNull(definition.Fields);
        }
    }
}