using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using DataProvider.EntityFrameworkCore.EntityMappings.Common;
using DataProvider.EntityFrameworkCore.EntityMappings.Localization;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class LocalizationContext : DbContext
    {
        public LocalizationContext() { }

        public LocalizationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StateEntityMapping());
            modelBuilder.ApplyConfiguration(new LocalizationValueEntityMapping());
        }

        public DbSet<LocalizationValueEntity> Translations { get; set; }

        public DbSet<StateEntity> States { get; set; }
    }
}
