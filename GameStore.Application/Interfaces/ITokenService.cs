using GameStore.Application.Models;
using GameStore.Domain.Entities;

namespace GameStore.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string userName, string email);
}