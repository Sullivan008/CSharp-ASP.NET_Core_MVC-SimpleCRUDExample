using System.Threading.Tasks;
using Data.DataAccessLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCRUDExample.Core.Middlewares
{
    public class ApplicationDeploy
    {
        private readonly RequestDelegate _next;

        public ApplicationDeploy(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (IServiceScope serviceScope = httpContext.RequestServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (CRUDAppContext appContext = serviceScope.ServiceProvider.GetService<CRUDAppContext>())
                {
                    appContext.Database.Migrate();
                }
            }

            await _next(httpContext);
        }
    }
}
