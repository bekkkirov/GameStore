using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(i => i.Url)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(i => i.PublicId)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasIndex(i => i.PublicId)
               .IsUnique();
    }
}