using System;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HeaderAttributeTest
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HeaderAttribute).IsSealed);
        }
        
        [Fact]
        public void IsAnAttribute()
        {
            True(typeof(HeaderAttribute).IsAssignableTo(typeof(Attribute)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WilNotAcceptEmptyOrWhitespaceHeaderName(string headerName)
        {
            var exception = Throws<ArgumentException>(() =>
            {
                new HeaderAttribute(headerName);
            });

            Equal("Value cannot be null or whitespace. (Parameter 'headerName')", exception.Message);
            Equal("headerName", exception.ParamName);
        }
        
        [Fact]
        public void WillSetValidHeaderNameAsProperty()
        {
            HeaderAttribute definition = new("Custom");
            Equal("Custom", definition.HeaderName);
        }
    }
}