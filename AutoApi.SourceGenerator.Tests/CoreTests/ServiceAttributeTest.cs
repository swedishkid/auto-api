using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class ServiceAttributeTest
    {
        [Fact]
        public void CanCreateInstanceWithEmptyConstructor()
        {
            var serviceAttribute = new ServiceAttribute();
            Assert.NotNull(serviceAttribute);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptEmptyOrWhitespaceAlias(string alias)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ServiceAttribute(alias));
            Assert.NotNull(exception);
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'alias')", exception.Message);
            Assert.Equal("alias", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidAliasAndSetAsProperty()
        {
            const string alias = "CustomAlias";
            var serviceAttribute = new ServiceAttribute(alias);
            Assert.Equal("CustomAlias",serviceAttribute.Alias);
        }
        
        [Fact]
        public void IsSealed()
        {
            Assert.True(typeof(ServiceAttribute).IsSealed);
        }
        
        [Fact]
        public void IsAnAttribute()
        {
            Assert.True(typeof(ServiceAttribute).IsAssignableTo(typeof(Attribute)));
        }
    }
}