using Microsoft.EntityFrameworkCore;
using System;

namespace ParishForms.Common.Contracts.DataProviders
{
    /// <summary>
    /// Contract for creating an instance of an Entity Framework context
    /// This is used to ensure operations have their own instance
    /// Otherwise multi-threading doesnt work quite right.
    /// </summary>
    public interface IDbContextFactory<TContext> : IDisposable where TContext : DbContext, new()
    {
        /// <summary>
        /// Construct an Entity Framework DbContext 
        /// </summary>
        /// <returns></returns>
        TContext ConstructContext();
    }
}
