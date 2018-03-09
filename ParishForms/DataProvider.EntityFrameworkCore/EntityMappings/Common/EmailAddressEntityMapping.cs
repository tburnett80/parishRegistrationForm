using DataProvider.EntityFrameworkCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Common
{
    internal sealed class EmailAddressEntityMapping : IEntityTypeConfiguration<EmailAddressEntity>
    {
        public void Configure(EntityTypeBuilder<EmailAddressEntity> builder)
        {
            builder.ToTable("emailAddresses", "common")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EmailType)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("address")
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired();
        }
    }
}
