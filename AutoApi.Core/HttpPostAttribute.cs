using System;
using System.Net.Http;

namespace AutoApi
{
    public sealed class HttpPostAttribute : Attribute, IHttpMethod, IRouteTemplate
    {
        public HttpPostAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }
        
        public HttpMethod HttpMethod => HttpMethod.Post;
        
        public string RouteTemplate { get; }
    }
}