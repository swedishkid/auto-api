using System;
using System.Net.Http;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HttpDeleteAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HttpDeleteAttribute).IsSealed);
        }

        [Fact]
        public void MethodSetToCorrectHttpVerb()
        {
            var attribute = new HttpDeleteAttribute("");
            Equal(HttpMethod.Delete, attribute.HttpMethod);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptNullRouteTemplate(string routeTemplate)
        {
            var exception = Throws<ArgumentNullException>(() => new HttpDeleteAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidNameAndSetAsProperty()
        {
            var attribute = new HttpDeleteAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}