using Data.DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Deploy.ApplicationDeploy
{
    public class ApplicationDeploy
    {
        /// <summary>
        ///     A DB Migration-t végrehajtó metódus.
        /// </summary>
        /// <param name="app">Objektum, amely biztosítja az alkalmazás konfigurálására szolgáló mechanizmusokat.</param>
        public static void DataBaseMigration(IApplicationBuilder app)
        {
            /// Hozzáférés biztosítása a service csomagokhoz.
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                /// Context kiolvasása a service csomagokból.
                using (FormulaContext context = serviceScope.ServiceProvider.GetService<FormulaContext>())
                {
                    /// Migration végrehajtása.
                    context.Database.Migrate();
                }
            }
        }
    }
}
