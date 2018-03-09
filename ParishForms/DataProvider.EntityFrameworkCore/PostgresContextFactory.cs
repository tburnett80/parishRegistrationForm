using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.DataProviders;
using System;
using ParishForms.Common.Models;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class PostgresContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext, new()
    {
        private readonly DbContextOptions<TContext> _options;

        public PostgresContextFactory(ConfigSettingsDto settings)
        {
            if(settings == null)
                throw new ArgumentNullException(nameof(settings));

            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseNpgsql(settings.ConnectionString);

            _options = builder.Options;
        }

        public TContext ConstructContext()
        {
            return (TContext) Activator.CreateInstance(typeof(TContext), _options);
        }

        public void Dispose()
        {
        }
    }
}
