using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoApi.Sample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AutoApi.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "AutoApi.Sample", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoApi.Sample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAutoApi();
            });
        }
    }

    public static class MapAutoApiExtension
    {
        public static void MapAutoApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.Map("/items", MyAutoApiRouteHandlers.GetItems)
                .WithMetadata(new Microsoft.AspNetCore.Mvc.HttpGetAttribute());
        }

        private class MyAutoApiRouteHandlers
        {
            public static async Task GetItems(HttpContext context)
            {
                var handler = context.RequestServices.GetRequiredService<ItemHandler>();
                var result = await handler.GetItems()
                    .ConfigureAwait(false);
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                await JsonSerializer.SerializeAsync(context.Response.Body, result)
                    .ConfigureAwait(false);
            }
        }
    }
}