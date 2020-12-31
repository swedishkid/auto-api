using System;
using System.Net.Http;

namespace AutoApi
{
    public sealed class HttpPatchAttribute : Attribute, IHttpMethod, IRouteTemplate
    {
        public HttpPatchAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }
        
        public HttpMethod HttpMethod => HttpMethod.Patch;
        
        public string RouteTemplate { get; }
    }
}