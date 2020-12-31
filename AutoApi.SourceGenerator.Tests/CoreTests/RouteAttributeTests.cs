using System;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests.CoreTests
{
    public class RouteAttributeTests
    {
        [Fact]
        public void IsSealed()
        {
            True(typeof(RouteAttribute).IsSealed);
        }

        [Theory]
        [InlineData(null)]
        public void WillNotAcceptEmptyOrWhitespaceName(string routeTemplate)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new RouteAttribute(routeTemplate));
            Equal("Value cannot be null. (Parameter 'routeTemplate')", exception.Message);
            Equal("routeTemplate", exception.ParamName);
        }

        [Fact]
        public void WillAcceptValidRouteTemplateAndSetAsProperty()
        {
            var attribute = new RouteAttribute("CustomRouteTemplate");
            Equal("CustomRouteTemplate", attribute.RouteTemplate);
        }
    }
}