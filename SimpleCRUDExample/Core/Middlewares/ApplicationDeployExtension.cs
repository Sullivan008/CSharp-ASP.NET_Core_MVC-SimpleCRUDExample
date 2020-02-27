using Microsoft.AspNetCore.Builder;

namespace SimpleCRUDExample.Core.Middlewares
{
    public static class ApplicationDeployExtension
    {
        public static IApplicationBuilder UseApplicationDeploy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationDeploy>();
        }
    }
}