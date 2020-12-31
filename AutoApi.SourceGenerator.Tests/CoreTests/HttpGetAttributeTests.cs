using System;
using System.Net.Http;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class HttpGetAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(HttpGetAttribute).IsSealed);
        }

        [Fact]
        public void MethodSetToCorrectHttpVerb()
        {
            var attribute = new HttpGetAttribute("");
            Equal(HttpMethod.Get, attribute.HttpMethod);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptNullRouteTemplate(string routeTemplate)
        {
            var exception = Throws<ArgumentNullException>(() => new HttpGetAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidNameAndSetAsProperty()
        {
            var attribute = new HttpGetAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}