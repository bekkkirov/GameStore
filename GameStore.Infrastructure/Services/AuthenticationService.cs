using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly SignInManager<UserIdentity> _signInManager;

    public AuthenticationService(IUnitOfWork unitOfWork,
                                 ITokenService tokenService,
                                 IMapper mapper,
                                 UserManager<UserIdentity> userManager,
                                 SignInManager<UserIdentity> signInManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string> SignInAsync(SignInModel signInData)
    {
        var user = await _userManager.FindByNameAsync(signInData.UserName);

        if (user is null)
        {
            throw new IdentityException("User with specified username doesn't exist.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, signInData.Password, false);

        if (!result.Succeeded)
        {
            throw new IdentityException("Invalid password.");
        }

        return _tokenService.GenerateAccessToken(user.UserName, user.Email);
    }

    public async Task<string> SignUpAsync(SignUpModel signUpData)
    {
        var identityToAdd = new UserIdentity() {UserName = signUpData.UserName, Email = signUpData.Email};

        var result = await _userManager.CreateAsync(identityToAdd, signUpData.Password);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors.FirstOrDefault()?.Description);
        }

        var userToAdd = _mapper.Map<User>(signUpData);

        _unitOfWork.UserRepository.Add(userToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _tokenService.GenerateAccessToken(identityToAdd.UserName, identityToAdd.Email);
    }
}