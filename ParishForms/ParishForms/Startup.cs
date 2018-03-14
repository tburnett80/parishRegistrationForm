using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Contracts.Managers;

namespace ParishForms
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IoC.DependencyInjector.AddServices(services, Configuration);
            services.AddMvc();

            //pre-cache localization data
            Task.Factory.StartNew(() =>
            {
                var provider = services.BuildServiceProvider();

                //ensure db and tables exist.
                using (var factory = provider.GetService<IDbContextFactory<CreationContext>>())
                using (var ctx = factory.ConstructContext())
                { }

                //load localization values into cache
                var loc = provider.GetService<ILocalizationManager>();
                loc.PreLoadCache().Wait();
            }, TaskCreationOptions.LongRunning);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
