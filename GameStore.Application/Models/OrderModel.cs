namespace GameStore.Application.Models;

public class OrderModel
{
    public int Id { get; set; }

    public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
}