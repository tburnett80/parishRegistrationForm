using DataProvider.EntityFrameworkCore.Entities.Exports;
using DataProvider.EntityFrameworkCore.EntityMappings.Exports;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class ExportContext : DbContext
    {
        public ExportContext() { }

        public ExportContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ExportQueueEntityMapping());
        }

        public DbSet<ExportQueueEntity> ExportQueue { get; set; }
    }
}
