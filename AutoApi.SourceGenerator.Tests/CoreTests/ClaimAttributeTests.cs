using System;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class ClaimAttributeTests
    {
        [Fact]
        public void AcceptValidClaimNameAndSetAsProperty()
        {
            const string claimName = "username";
            var attribute = new ClaimAttribute(claimName);
            Assert.Equal("username", attribute.ClaimName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void WillNotAcceptNullOrWhitespaceClaimName(string claimName)
        {
            var exception = Assert.Throws<ArgumentException>(() => new ClaimAttribute(claimName));
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'claimName')", exception.Message);
            Assert.Equal("claimName", exception.ParamName);
        }

        [Fact]
        public void IsSealed()
        {
            Assert.True(typeof(ClaimAttribute).IsSealed);
        }
    }
}