using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFrameworkCore
{
    public sealed class DirectoryContext : DbContext
    {
        public DirectoryContext(DbContextOptions<DirectoryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new PersonTypeEntityMapping());
        }
        
        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<EmailAddressEntity> EmailAddress { get; set; }

        public DbSet<StateEntity> States { get; set; }

        public DbSet<PhoneEntity> Phones { get; set; }

        public DbSet<SubmisionEntitiy> Submisions { get; set; }
    }
}
