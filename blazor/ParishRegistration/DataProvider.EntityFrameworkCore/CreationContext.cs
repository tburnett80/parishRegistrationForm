using DataProvider.EntityFrameworkCore.EntityMappings.Common;
using DataProvider.EntityFrameworkCore.EntityMappings.Directory;
using DataProvider.EntityFrameworkCore.EntityMappings.Exports;
using DataProvider.EntityFrameworkCore.EntityMappings.Localization;
using DataProvider.EntityFrameworkCore.EntityMappings.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    /// <summary>
    /// This context is only used for creating the database
    /// Because we only use this to init the database, it contains no DbSet objects
    /// </summary>
    public class CreationContext : DbContext
    {
        public CreationContext() { }

        public CreationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExportQueueEntityMapping());

            modelBuilder.ApplyConfiguration(new LogHeaderEntityMapping());
            modelBuilder.ApplyConfiguration(new LogDetailEntityMapping());

            modelBuilder.ApplyConfiguration(new CultureEntityMapping());
            modelBuilder.ApplyConfiguration(new LocalizationValueEntityMapping());

            modelBuilder.ApplyConfiguration(new StateEntityMapping());
            modelBuilder.ApplyConfiguration(new PhoneEntityMapping());
            modelBuilder.ApplyConfiguration(new EmailAddressEntityMapping());
            modelBuilder.ApplyConfiguration(new AddressEntityMapping());

            modelBuilder.ApplyConfiguration(new SubmisionEntitiyMapping());
        }
    }
}
