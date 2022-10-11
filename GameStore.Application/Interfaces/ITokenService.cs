using GameStore.Application.Models;
using GameStore.Domain.Entities;

namespace GameStore.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(UserClaimsModel claimsModel);
}