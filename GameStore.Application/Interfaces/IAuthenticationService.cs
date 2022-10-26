using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface IAuthenticationService
{
    public Task<string> SignInAsync(SignInModel signInData);

    public Task<string> SignUpAsync(SignUpModel signUpData);
}