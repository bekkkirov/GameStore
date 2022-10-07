using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasIndex(g => g.Key)
               .IsUnique();

        builder.Property(g => g.Key)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(g => g.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(g => g.Description)
               .HasMaxLength(500)
               .IsRequired();

        builder.HasOne(g => g.Image)
               .WithOne(i => i.Game)
               .HasForeignKey<Image>(i => i.GameId);

        builder.HasMany(g => g.Genres)
               .WithMany(g => g.Games);

        builder.HasMany(g => g.PlatformTypes)
               .WithMany(pt => pt.Games);
    }
}