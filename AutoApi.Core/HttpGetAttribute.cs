using System;
using System.Net.Http;

namespace AutoApi
{
    public sealed class HttpGetAttribute : Attribute, IHttpMethod, IRouteTemplate
    {
        public string RouteTemplate { get; }

        public HttpGetAttribute(string routeTemplate)
        {
            RouteTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }

        public HttpMethod HttpMethod => HttpMethod.Get;
    }
}