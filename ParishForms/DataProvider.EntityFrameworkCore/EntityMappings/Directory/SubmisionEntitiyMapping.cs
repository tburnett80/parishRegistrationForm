using DataProvider.EntityFrameworkCore.Entities.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProvider.EntityFrameworkCore.EntityMappings.Directory
{
    internal sealed class SubmisionEntitiyMapping : IEntityTypeConfiguration<SubmisionEntitiy>
    {
        public void Configure(EntityTypeBuilder<SubmisionEntitiy> builder)
        {
            builder.ToTable("submisions", "directory")
                .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Timestamp)
                .HasColumnName("dtmCreated")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.AddressId)
                .HasColumnName("addressId")
                .IsRequired();

            builder.Property(e => e.HomePhoneId)
                .HasColumnName("homePhoneId")
                .IsRequired();

            builder.Property(e => e.AdultOnePhoneId)
                .HasColumnName("adultOnePhoneId")
                .IsRequired();

            builder.Property(e => e.AdultTwoPhoneId)
                .HasColumnName("adultTwoPhoneId")
                .IsRequired();

            builder.Property(e => e.AdultOneEmailId)
                .HasColumnName("adultOneEmailId")
                .IsRequired();

            builder.Property(e => e.AdultTwoEmailId)
                .HasColumnName("adultTwoEmailId")
                .IsRequired();

            builder.Property(e => e.PublishPhone)
                .HasColumnName("publishPhone")
                .IsRequired();

            builder.Property(e => e.PublishAddress)
                .HasColumnName("publishAddress")
                .IsRequired();

            builder.Property(e => e.AdultOneFirstName)
                .HasColumnName("adult1")
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.AdultTwoFirstName)
                .HasColumnName("adult2")
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.OtherFamily)
                .HasColumnName("others")
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();
        }
    }
}
