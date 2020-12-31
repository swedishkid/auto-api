using System;
using System.Net.Http;

namespace AutoApi
{
    public sealed class HttpDeleteAttribute : Attribute, IHttpMethod, IRouteTemplate
    {
        public HttpDeleteAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }
        
        public HttpMethod HttpMethod => HttpMethod.Delete;
        
        public string RouteTemplate { get; }
    }
}