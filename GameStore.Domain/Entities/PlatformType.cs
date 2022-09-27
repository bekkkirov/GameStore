using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class PlatformType : BaseEntity
{
    public string Type { get; set; }

    public List<Game> Games { get; set; } = new List<Game>();
}