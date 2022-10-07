namespace GameStore.Application.Options;

public class DbConnectionOptions
{
    public const string SectionName = "ConnectionStrings"; 

    public string GameStore { get; set; } = string.Empty;
}