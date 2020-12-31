using System;
using System.Net.Http;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HttpPostAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HttpPostAttribute).IsSealed);
        }

        [Fact]
        public void MethodSetToCorrectHttpVerb()
        {
            var attribute = new HttpPostAttribute("");
            Equal(HttpMethod.Post, attribute.HttpMethod);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptNullOrWhitespaceRouteTemplate(string routeTemplate)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new HttpPostAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidRouteTemplateAndSetAsProperty()
        {
            var attribute = new HttpPostAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}