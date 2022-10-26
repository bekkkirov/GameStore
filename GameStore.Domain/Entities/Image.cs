using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Image : BaseEntity
{
    public string Url { get; set; }

    public string PublicId { get; set; }

    public int? GameId { get; set; }
    public Game Game { get; set; }

    public int? UserId { get; set; }
    public User User { get; set; }
}