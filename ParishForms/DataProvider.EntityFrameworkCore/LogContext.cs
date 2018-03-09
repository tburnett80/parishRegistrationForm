using DataProvider.EntityFrameworkCore.Entities.Logging;
using DataProvider.EntityFrameworkCore.EntityMappings.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LogHeaderEntityMapping());
            modelBuilder.ApplyConfiguration(new LogDetailEntityMapping());
        }

        public DbSet<LogHeaderEntity> LogHeaders { get; set; }

        public DbSet<LogDetailEntity> LogDetails { get; set; }
    }
}
