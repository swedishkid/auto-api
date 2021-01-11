using System.Threading;
using AutoApi.Generated;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace AutoApi.Sample
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder<Startup>(args);
            var host = builder.Build();
            host.Run();
        }
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}