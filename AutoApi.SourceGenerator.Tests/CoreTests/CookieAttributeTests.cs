using System;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class CookieAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            Assert.True(typeof(CookieAttribute).IsSealed);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptEmptyOrWhitespaceName(string name)
        {
            var exception = Assert.Throws<ArgumentException>(() => new CookieAttribute(name));
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'name')", exception.Message);
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidNameAndSetAsProperty()
        {
            var attribute = new CookieAttribute("CustomName");
            Assert.Equal("CustomName", attribute.Name);
        }

        [Fact]
        public void CanCreateInstanceWithEmptyConstructor()
        {
            var attribute = new CookieAttribute();
            Assert.Null(attribute.Name);
        }
    }
}