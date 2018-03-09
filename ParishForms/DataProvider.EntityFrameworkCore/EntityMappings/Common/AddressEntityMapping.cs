using DataProvider.EntityFrameworkCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Common
{
    internal sealed class AddressEntityMapping : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("address", "common")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.AddressType)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(e => e.StateId)
                .HasColumnName("stateId")
                .IsRequired();

            builder.Property(e => e.Street)
                .HasColumnName("street")
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.City)
                .HasColumnName("city")
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.Zip)
                .HasColumnName("zip")
                .HasMaxLength(10)
                .IsUnicode()
                .IsRequired();
        }
    }
}
