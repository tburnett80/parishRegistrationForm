using DataProvider.EntityFrameworkCore.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Localization
{
    internal class LocalizationValueEntityMapping : IEntityTypeConfiguration<LocalizationValueEntity>
    {
        public void Configure(EntityTypeBuilder<LocalizationValueEntity> builder)
        {
            builder.ToTable("translations")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.KeyCultureId)
                .HasColumnName("keyCultureId")
                .IsRequired();

            builder.Property(e => e.TranslationCultureId)
                .HasColumnName("tranCultureId")
                .IsRequired();

            builder.Property(e => e.KeyText)
                .HasColumnName("key")
                .HasMaxLength(4000)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.TranslationText)
                .HasColumnName("value")
                .HasMaxLength(4000)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.Created)
                .HasColumnName("dtmCreated")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.LastModified)
                .HasColumnName("dtmUpdated")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(e => e.KeyCulture)
                .WithMany()
                .HasForeignKey(e => e.KeyCultureId);

            builder.HasOne(e => e.TranslationCulture)
                .WithMany()
                .HasForeignKey(e => e.TranslationCultureId);
        }
    }
}
