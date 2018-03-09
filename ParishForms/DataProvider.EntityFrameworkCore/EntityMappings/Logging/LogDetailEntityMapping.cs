using DataProvider.EntityFrameworkCore.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Logging
{
    internal sealed class LogDetailEntityMapping : IEntityTypeConfiguration<LogDetailEntity>
    {
        public void Configure(EntityTypeBuilder<LogDetailEntity> builder)
        {
            builder.ToTable("detail", "logging")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HeaderId)
                .HasColumnName("headerId")
                .IsRequired();

            builder.Property(e => e.EventType)
                .HasColumnName("eventType")
                .IsRequired();

            builder.Property(e => e.EventText)
                .HasColumnName("event")
                .IsUnicode()
                .HasMaxLength(4000)
                .IsRequired();
        }
    }
}
