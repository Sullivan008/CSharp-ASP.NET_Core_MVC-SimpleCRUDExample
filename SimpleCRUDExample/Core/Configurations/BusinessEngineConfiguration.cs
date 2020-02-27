using Business.Engine.Engines;
using Business.Engine.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCRUDExample.Core.Configurations
{
    public static class BusinessEngineConfiguration
    {
        public static IServiceCollection ConfigureBusinessEngines(this IServiceCollection services)
        {
            services.AddScoped<IUserEngine, UserEngine>();
            services.AddScoped<ITeamEngine, TeamEngine>();

            return services;
        }
    }
}
