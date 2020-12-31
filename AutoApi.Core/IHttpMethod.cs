using System.Net.Http;

namespace AutoApi
{
    public interface IHttpMethod
    {
        HttpMethod HttpMethod { get; }
    }
}