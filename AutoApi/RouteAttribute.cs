using System;

namespace AutoApi
{
    public class RouteAttribute : Attribute
    {
        public string RouteTemplate { get; }

        public RouteAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate;
        }
    }
}