using Business.Entities.DataBaseEntities;
using Core.Deploy.ApplicationDeploy;
using Core.Handlers.BackEndExceptionHandler;
using Data.DataAccessLayer.Context;
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
        private SqliteConnection inMemorySqlite;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                inMemorySqlite = new SqliteConnection(Configuration.GetConnectionString("DevConnection"));
                inMemorySqlite.Open();

                services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<FormulaContext>();

                services.AddDbContext<FormulaContext>(options =>
                    options.UseSqlite(inMemorySqlite));
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba! Hiba az adatbázis inicializálása során!");
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

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

            app.UseAuthentication();

            ApplicationDeploy.DataBaseMigration(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Team}/{action=Index}/{id?}");
            });
        }

        private static void DataBaseMigration(IApplicationBuilder app)
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
