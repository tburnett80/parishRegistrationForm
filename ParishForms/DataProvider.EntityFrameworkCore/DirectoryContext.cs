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

            //Add table mappings here
            //modelBuilder.ApplyConfiguration(new PersonTypeEntityMapping());
        }
    }
}
