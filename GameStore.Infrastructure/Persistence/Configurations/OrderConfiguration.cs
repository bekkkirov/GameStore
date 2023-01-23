using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.FirstName)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(o => o.LastName)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(o => o.Phone)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(o => o.Email)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(o => o.Comment)
               .HasMaxLength(600);

        builder.Property(o => o.PaymentType)
               .HasConversion(
                   v => v.ToString(),
                   v => (PaymentType)Enum.Parse(typeof(PaymentType), v));

        builder.HasOne(o => o.Cart)
               .WithOne()
               .HasForeignKey<Order>(o => o.CartId);

        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}