using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<string>> SignIn(SignInModel signInData)
    {
        var token = await _authenticationService.SignInAsync(signInData);

        return Ok(token);
    } 
    
    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<string>> SignUp(SignUpModel signUpData)
    {
        var token = await _authenticationService.SignUpAsync(signUpData);

        return Ok(token);
    }
}