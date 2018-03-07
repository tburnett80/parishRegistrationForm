using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ParishForms.IoC
{
    public static class DependencyInjector
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<ConfigSettings>(new ConfigSettings
            //{
            //    DbConnStr = configuration["ConfigSettings:DbConnStr"]
            //});

            //services.AddTransient<IHttpHandler, HttpHandler>();
            //services.AddSingleton<ICircuitBreaker<OmdbApiSource>>(new CircuitBreaker<OmdbApiSource>(600000, 5000, 4));
        }
    }
}
