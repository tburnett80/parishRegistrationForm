using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Extensions;

namespace ParishForms.Tests
{
    /// <summary>
    /// This class should be use for tests that need the data to persist after the context is disposed
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public sealed class SharedSqliteInMemoryContextFactory<TContext> : SqliteInMemoryContextFactory<TContext> where TContext : DbContext, new()
    {
        private static SQLiteConnection _conn;
        private bool IsInitialized { get; set; }

        public SharedSqliteInMemoryContextFactory()
            :base(BuildOptions())
        {
        }

        public override void Dispose()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                _conn.Close();

            _conn?.Dispose();
        }

        public override TContext ConstructContext()
        {
            if (IsInitialized)
                return (TContext) Activator.CreateInstance(typeof(TContext), Options);

            IsInitialized = true;
            return base.ConstructContext();
        }

        private static DbContextOptions<TContext> BuildOptions()
        {
            _conn = new SQLiteConnection($"DataSource={Guid.NewGuid().GuidToId()};mode=memory;cache=shared;");
            _conn.Open();

            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseSqlite(_conn);

            return builder.Options;
        }
    }
}
