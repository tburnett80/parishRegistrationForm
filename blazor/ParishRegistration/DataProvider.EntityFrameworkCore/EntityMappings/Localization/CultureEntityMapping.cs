using DataProvider.EntityFrameworkCore.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Localization
{
    internal class CultureEntityMapping : IEntityTypeConfiguration<CultureEntity>
    {
        public void Configure(EntityTypeBuilder<CultureEntity> builder)
        {
            builder.ToTable("cultures")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CultureCode)
                .HasColumnName("code")
                .HasMaxLength(8)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.CultureName)
                .HasColumnName("name")
                .HasMaxLength(32)
                .IsUnicode()
                .IsRequired();
        }
    }
}
