namespace GameStore.Application.Options;

public class DbConnectionOptions
{
    public const string SectionName = "ConnectionStrings"; 

    public string GameStore { get; set; } = string.Empty;

    public string Identity { get; set; } = string.Empty;
}