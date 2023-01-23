using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public bool IsOrdered { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}