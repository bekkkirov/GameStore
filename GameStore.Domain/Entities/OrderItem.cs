using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Amount { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public int CartId { get; set; }
    public Cart Cart { get; set; }
}