using Business.Entities.DataBaseEntities;
using Core.Deploy.ApplicationDeploy;
using Core.Handlers.BackEndExceptionHandler;
using Data.DataAccessLayer.Context;
using FormulaOneProject.Models.ConfigModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FormulaOneProject
{
    public class Startup
    {
        /// Az InMemory Sqlite-hoz tartozó objektum.
        private SqliteConnection inMemorySqlite;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     A metódus segítségével, Szolgáltatásokat adhatunk a Konténerhez.
        /// </summary>
        /// <param name="services">Meghatározza a szolgáltatási leírás gyűjteményét</param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                /// Sqlite objektum példányosítása, majd konfigurálása az appsettings.json-ban meghatározott Connection String-gel.
                /// majd a kapcsolat nyitása.
                inMemorySqlite = new SqliteConnection(Configuration.GetConnectionString("DevConnection"));
                inMemorySqlite.Open();

                /// Az azonosítási rendszer konfigurációja és hozzáadása a szolgáltatási gyűjteményhez és
                /// az azonosítási információk megvalósítása EntityFramework-ben meghatározott T típusú Context-ben.
                services.AddIdentity<AppUser, AppRole>(options =>
                {
                    /// Kötelező Egyedi E-mail cím konfigurációja.
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<FormulaContext>();

                /// A T Típusú (FormulaContext) Context konfigurációja hogy a Context egy SQLite adatbázishoz kapcsolódjon
                /// majd hozzáadása a szolgáltatási gyűjteményhe.
                services.AddDbContext<FormulaContext>(options =>
                    options.UseSqlite(inMemorySqlite));
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba! Hiba az adatbázis inicializálása során!");
            }

            /// A személy azonosítási rendszer konfigurálása:
            ///     - Az Alphanumerikus karakterek engedélyezése.
            ///     - Letiltjuk azt, hogy a jelszónak szükséges tartalmaznia legalább 1 nagybetűs karaktert.
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /// Kiolvassuk az application.json fájlból a Login-hoz szükséges információkat, majd ezt a LoginConfigModel-ben
            /// tárolni is fogjuk.
            services.Configure<LoginConfigModel>(Configuration.GetSection("DevLoginConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            /// Hitelesítési rendszer használata.
            app.UseAuthentication();

            /// Az adatbázis migrációjának lefuttatása.
            ApplicationDeploy.DataBaseMigration(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Team}/{action=Index}/{id?}");
            });
        }
    }
}
