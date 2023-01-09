using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Order : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Comment { get; set; }

    public PaymentType PaymentType { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}