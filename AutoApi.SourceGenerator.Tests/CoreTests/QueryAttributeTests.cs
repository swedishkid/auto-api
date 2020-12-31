using System;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class QueryAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(QueryAttribute).IsSealed);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptNullOrWhitespaceRouteTemplate(string key)
        {
            var exception = Throws<ArgumentException>(() => new QueryAttribute(key));
            Equal("Value cannot be null or whitespace. (Parameter 'key')", exception.Message);
            Equal("key", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidKeyAndSetAsProperty()
        {
            var attribute = new QueryAttribute("CustomKey");
            Equal("CustomKey", attribute.Key);
        }
        
        [Fact]
        public void CanCreateInstanceFromEmptyConstructor()
        {
            var attribute = new QueryAttribute();
            Null(attribute.Key);
        }
    }
}