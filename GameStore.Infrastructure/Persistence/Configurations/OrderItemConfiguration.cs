using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(i => i.Amount)
               .IsRequired();

        builder.HasOne(i => i.Cart)
               .WithMany(o => o.Items)
               .HasForeignKey(i => i.CartId);

        builder.HasOne(i => i.Game)
               .WithMany()
               .HasForeignKey(i => i.GameId);
    }
}