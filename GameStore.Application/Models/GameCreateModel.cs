namespace GameStore.Application.Models;

public class GameCreateModel
{
    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public List<int> GenreIds { get; set; }

    public List<int> PlatformIds { get; set; }
}