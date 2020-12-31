using System;
using AutoApi.SourceGenerator.Definition;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.DefinitionTests
{
    public class InterfaceDefinitionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptEmptyOrWhitespaceClassName(string interfaceName)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                new InterfaceDefinition(interfaceName);
            });
            
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'interfaceName')" , exception.Message);
            Assert.Equal("interfaceName", exception.ParamName);
        }

        [Fact]
        public void WillSetValidClassNameAsProperty()
        {
            InterfaceDefinition definition = new("Custom");
            Assert.Equal("Custom", definition.InterfaceName);
        }
        
        [Fact]
        public void AttributeCollectionIsSet()
        {
            InterfaceDefinition definition = new("Custom");
            Assert.NotNull(definition.Attributes);
        }
        
        [Fact]
        public void PropertiesCollectionIsSet()
        {
            InterfaceDefinition definition = new("Custom");
            Assert.NotNull(definition.Properties);
        }
    }
}