using DataProvider.EntityFrameworkCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Common
{
    internal sealed class PhoneEntityMapping : IEntityTypeConfiguration<PhoneEntity>
    {
        public void Configure(EntityTypeBuilder<PhoneEntity> builder)
        {
            builder.ToTable("phones", "common")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TypeId)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(e => e.Number)
                .HasColumnName("number")
                .HasMaxLength(15)
                .IsUnicode()
                .IsRequired();
        }
    }
}
