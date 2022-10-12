using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.UserName)
               .IsUnique();

        builder.Property(u => u.UserName)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(u => u.FirstName)
               .HasMaxLength(30)
               .IsRequired();

        builder.Property(u => u.LastName)
               .HasMaxLength(30)
               .IsRequired();

        builder.HasOne(u => u.ProfileImage)
               .WithOne(i => i.User)
               .HasForeignKey<Image>(i => i.UserId);
    }
}