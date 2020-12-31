using System;

namespace AutoApi
{
    public sealed class RouteAttribute : Attribute, IRouteTemplate
    {
        public RouteAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }
        
        public string RouteTemplate { get; }
    }
}