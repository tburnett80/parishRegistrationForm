using DataProvider.EntityFrameworkCore.Entities.Exports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Exports
{
    internal class ExportQueueEntityMapping : IEntityTypeConfiguration<ExportQueueEntity>
    {
        public void Configure(EntityTypeBuilder<ExportQueueEntity> builder)
        {
            builder.ToTable("exportQueue")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.RequestId)
                .HasColumnName("requestGuid")
                .IsRequired();

            builder.Property(e => e.ExportType)
                .HasColumnName("exportType")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(e => e.TimeStamp)
                .HasColumnName("created")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("ownerId")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.StartRange)
                .HasColumnName("startRange");

            builder.Property(e => e.LastUpdated)
                .HasColumnName("lastUpdated")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}