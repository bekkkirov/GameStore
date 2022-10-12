using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _tokenOptions;

    public TokenService(IOptions<JwtOptions> tokenOptions)
    {
        _tokenOptions = tokenOptions;
    }

    public string GenerateAccessToken(string userName, string email)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email)
        };

        var bytes = Encoding.UTF8.GetBytes(_tokenOptions.Value.Key);
        var key = new SymmetricSecurityKey(bytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(_tokenOptions.Value.ExpiresInDays),
            SigningCredentials = credentials,
            Issuer = _tokenOptions.Value.Issuer,
            Audience = _tokenOptions.Value.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}