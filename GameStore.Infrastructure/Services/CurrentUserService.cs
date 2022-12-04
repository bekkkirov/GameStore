using System.Security.Claims;
using GameStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _currentUser;

    public CurrentUserService(IHttpContextAccessor context)
    {
        _currentUser = context.HttpContext?.User;
    }

    public string GetUsername()
    {
        return _currentUser.FindFirstValue(ClaimTypes.Name);
    }
}