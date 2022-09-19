using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }

    public int? ParentGenreId { get; set; }
    public Genre ParentGenre { get; set; }

    public List<Game> Games { get; set; } = new List<Game>();

    public List<Genre> SubGenres { get; set; } = new List<Genre>();
}