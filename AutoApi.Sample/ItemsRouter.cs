using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AutoApi.Sample
{
    public class ItemsRouterWrapper
    {
        public void Map(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            var routeBuilder = new Microsoft.AspNetCore.Routing.RouteBuilder(app);
            Microsoft.AspNetCore.Routing.RequestDelegateRouteBuilderExtensions.MapGet(routeBuilder, "items", MapGetItems);
            var router = routeBuilder.Build();
            Microsoft.AspNetCore.Builder.RoutingBuilderExtensions.UseRouter(app, router);
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