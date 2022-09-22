namespace GameStore.Application.Models;

public class GameModel
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Price { get; set; }

    public List<GenreModel> Genres { get; set; }

    public List<PlatformTypeModel> PlatformTypes { get; set; }
}