using System.Security.Claims;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    [Route("currentUser")]
    public async Task<ActionResult<UserModel>> GetCurrentUser()
    {
        var user = await _userService.GetCurrentUserInfoAsync();

        return Ok(user);
    }

    [HttpGet]
    [Route("{userName}")]
    public async Task<ActionResult<UserModel>> GetByUserName(string userName)
    {
        var user = await _userService.GetUserInfoAsync(userName);

        return Ok(user);
    }

    [HttpPost]
    [Route("image")]
    public async Task<ActionResult<ImageModel>> SetProfileImage(IFormFile image)
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);

        var created = await _userService.SetProfileImageAsync(userName, image);

        return CreatedAtAction(nameof(GetByUserName), new { UserName = userName }, created);
    }
}