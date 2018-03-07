using DataProvider.EntityFrameworkCore.Entities.Localization;
using DataProvider.EntityFrameworkCore.EntityMappings.Localization;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class LocalizationContext : DbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add table mappings here
            modelBuilder.ApplyConfiguration(new LocalizationValueEntityMapping());
        }

        public DbSet<LocalizationValueEntity> Translations { get; set; }
    }
}
