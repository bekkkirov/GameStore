using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Game : BaseEntity
{
    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();

    public List<Genre> Genres { get; set; } = new List<Genre>();

    public List<PlatformType> PlatformTypes { get; set; } = new List<PlatformType>();
}