using System;
using System.Net.Http;

namespace AutoApi
{
    public sealed class HttpPutAttribute : Attribute, IHttpMethod, IRouteTemplate
    {
        public HttpPutAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }
        
        public HttpMethod HttpMethod => HttpMethod.Put;
        
        public string RouteTemplate { get; }
    }
}