using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Body)
               .HasMaxLength(250)
               .IsRequired();

        builder.HasOne(c => c.Game)
               .WithMany(g => g.Comments)
               .HasForeignKey(c => c.GameId);

        builder.HasOne(c => c.Author)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.AuthorId);

        builder.HasMany(c => c.Replies)
               .WithOne(c => c.ParentComment)
               .HasForeignKey(c => c.ParentCommentId);
    }
}