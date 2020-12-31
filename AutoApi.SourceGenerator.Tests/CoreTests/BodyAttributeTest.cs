using System;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class BodyAttributeTest
    {
        [Fact]
        public void CanCreateInstanceFromEmptyConstructor()
        {
            var bodyAttribute = new BodyAttribute();
            Assert.NotNull(bodyAttribute);
        }
        
        [Fact]
        public void IsSealed()
        {
            Assert.True(typeof(BodyAttribute).IsSealed);
        }
        
        [Fact]
        public void IsAnAttribute()
        {
            Assert.True(typeof(BodyAttribute).IsAssignableTo(typeof(Attribute)));
        }
    }
}