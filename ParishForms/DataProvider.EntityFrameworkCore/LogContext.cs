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

            //Add table mappings here
            //modelBuilder.ApplyConfiguration(new PersonTypeEntityMapping());
        }
    }
}
