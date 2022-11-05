namespace GameStore.Application.Models;

public class GameSearchOptions
{
    public string NamePattern { get; set; }

    public List<int> GenreIds { get; set; }
}