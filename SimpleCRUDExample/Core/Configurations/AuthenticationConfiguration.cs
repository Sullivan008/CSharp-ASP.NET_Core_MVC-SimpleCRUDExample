using Data.DataAccessLayer.Context;
using Data.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCRUDExample.Core.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthService(this IServiceCollection services)
        {
            services.AddIdentity<CRUDAppUser, CRUDAppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<CRUDAppContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            return services;
        }
    }
}
