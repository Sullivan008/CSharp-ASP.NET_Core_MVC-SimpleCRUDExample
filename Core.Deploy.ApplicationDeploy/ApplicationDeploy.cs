using System.Threading.Tasks;
using Data.DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Deploy.ApplicationDeploy
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (var serviceScope = httpContext.RequestServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (CRUDAppContext crudAppContext = serviceScope.ServiceProvider.GetService<CRUDAppContext>())
                {
                    crudAppContext.Database.Migrate();
                }
            }
            await _next(httpContext); // calling next middleware

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
