using GameStore.Domain.Entities;

namespace GameStore.Application.Models;

public class CreateOrderModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Comment { get; set; }

    public int PaymentType { get; set; }

    public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
}