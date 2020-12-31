using System;
using System.Net.Http;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HttpPutAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HttpPutAttribute).IsSealed);
        }

        [Fact]
        public void MethodSetToCorrectHttpVerb()
        {
            var attribute = new HttpPutAttribute("");
            Equal(HttpMethod.Put, attribute.HttpMethod);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptNullOrWhitespaceRouteTemplate(string routeTemplate)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new HttpPutAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidRouteTemplateAndSetAsProperty()
        {
            var attribute = new HttpPutAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}