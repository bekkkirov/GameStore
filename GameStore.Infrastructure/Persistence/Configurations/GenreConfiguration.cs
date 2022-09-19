using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasIndex(g => g.Name)
               .IsUnique();

        builder.Property(g => g.Name)
               .HasMaxLength(30)
               .IsRequired();

        builder.HasMany(g => g.SubGenres)
               .WithOne(g => g.ParentGenre)
               .HasForeignKey(g => g.ParentGenreId);
    }
}