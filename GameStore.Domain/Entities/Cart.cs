using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public Order Order { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}