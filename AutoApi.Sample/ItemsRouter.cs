using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AutoApi.Sample
{
    public class ItemsRouter
    {
        public void Map(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("items", MapGetItems);
            var router = routeBuilder.Build();
            app.UseRouter(router);
        }

        private async Task MapGetItems(HttpContext context)
        {
            var service_context = context.RequestServices.GetRequiredService<ItemsDbContext>();
            var handler = new Items(service_context);
            var result = await handler.GetItems(context.RequestAborted);
            await JsonSerializer.SerializeAsync(context.Response.Body, result);
        }
    }
}