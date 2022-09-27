namespace GameStore.API.Options;

public class DbConnectionOptions
{
    public const string SectionName = "ConnectionStrings"; 

    public string GameStore { get; set; } = string.Empty;
}