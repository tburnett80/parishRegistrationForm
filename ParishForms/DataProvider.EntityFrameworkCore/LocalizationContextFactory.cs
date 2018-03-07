using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.DataProviders;
using System;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class ContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<TContext> _options;

        public TContext ConstructContext<TContext>() where TContext : class
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
