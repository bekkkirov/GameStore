namespace GameStore.Application.Models;

public class OrderItemModel
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public int Amount { get; set; }
}