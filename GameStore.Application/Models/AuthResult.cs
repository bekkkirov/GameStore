namespace GameStore.Application.Models;

public class AuthResult
{
    public string Token { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ImageModel ProfileImage { get; set; }
}