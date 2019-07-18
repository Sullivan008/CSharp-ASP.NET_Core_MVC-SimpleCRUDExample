using Business.Entities.DataBaseEntities;
using Data.DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Deploy.ApplicationDeploy
{
    public class ApplicationDeploy
    {
        public static void DataBaseMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (FormulaContext context = serviceScope.ServiceProvider.GetService<FormulaContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
