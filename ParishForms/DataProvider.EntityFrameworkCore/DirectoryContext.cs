using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using DataProvider.EntityFrameworkCore.EntityMappings.Common;
using DataProvider.EntityFrameworkCore.EntityMappings.Directory;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class DirectoryContext : DbContext
    {
        public DirectoryContext() { }

        public DirectoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StateEntityMapping());
            modelBuilder.ApplyConfiguration(new PhoneEntityMapping());
            modelBuilder.ApplyConfiguration(new EmailAddressEntityMapping());
            modelBuilder.ApplyConfiguration(new AddressEntityMapping());
            modelBuilder.ApplyConfiguration(new SubmisionEntitiyMapping());
        }
        
        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<EmailAddressEntity> EmailAddress { get; set; }

        public DbSet<StateEntity> States { get; set; }

        public DbSet<PhoneEntity> Phones { get; set; }

        public DbSet<SubmisionEntitiy> Submisions { get; set; }
    }
}
