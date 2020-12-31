using System;
using System.Net.Http;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HttpPatchAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HttpPatchAttribute).IsSealed);
        }

        [Fact]
        public void MethodSetToCorrectHttpVerb()
        {
            var attribute = new HttpPatchAttribute("");
            Equal(HttpMethod.Patch, attribute.HttpMethod);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptNullOrWhitespaceRouteTemplate(string routeTemplate)
        {
            var exception = Throws<ArgumentNullException>(() => new HttpPatchAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidRouteTemplateAndSetAsProperty()
        {
            var attribute = new HttpPatchAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}