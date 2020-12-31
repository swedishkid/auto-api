using System;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class FormAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            Assert.True(typeof(FormAttribute).IsSealed);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptEmptyOrWhitespaceName(string key)
        {
            var exception = Assert.Throws<ArgumentException>(() => new FormAttribute(key));
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'key')", exception.Message);
            Assert.Equal("key", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidNameAndSetAsProperty()
        {
            var attribute = new FormAttribute("CustomKey");
            Assert.Equal("CustomKey", attribute.Key);
        }

        [Fact]
        public void CanCreateInstanceWithEmptyConstructor()
        {
            var attribute = new FormAttribute();
            Assert.Null(attribute.Key);
        }
    }
}