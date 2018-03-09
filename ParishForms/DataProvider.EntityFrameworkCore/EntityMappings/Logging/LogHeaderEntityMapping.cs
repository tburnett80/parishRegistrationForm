using DataProvider.EntityFrameworkCore.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Logging
{
    internal sealed class LogHeaderEntityMapping : IEntityTypeConfiguration<LogHeaderEntity>
    {
        public void Configure(EntityTypeBuilder<LogHeaderEntity> builder)
        {
            builder.ToTable("header", "logging")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Level)
                .HasColumnName("level")
                .IsRequired();

            builder.Property(e => e.Timestamp)
                .HasColumnName("eventTs")
                .ValueGeneratedOnAdd();

            builder.HasMany(e => e.Details)
                .WithOne(e => e.Header)
                .HasForeignKey(e => e.HeaderId);
        }
    }
}
