using DataProvider.EntityFrameworkCore;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Contracts.Managers;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ParishRegistration.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Wireup framework services
            ParishForms.IoC.DependencyInjector.AddServices(services, Configuration);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            //var provider = services.BuildServiceProvider();

            ////pre-cache localization data
            //Task.Factory.StartNew(() =>
            //{
            //    //ensure db and tables exist.
            //    using (var factory = provider.GetService<IDbContextFactory<CreationContext>>())
            //    using (var ctx = factory.ConstructContext())
            //    { }

            //    //load localization values into cache
            //    var loc = provider.GetService<ILocalizationManager>();
            //    loc.PreLoadCache().Wait();
            //}, TaskCreationOptions.LongRunning);

            ////start processing thread to handle creating exports
            //Task.Factory.StartNew(async () =>
            //{
            //    var mgr = provider.GetService<IExportProcessingManager>();
            //    await mgr.StartProcessing();
            //}, TaskCreationOptions.LongRunning);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Program>();
        }
    }
}
