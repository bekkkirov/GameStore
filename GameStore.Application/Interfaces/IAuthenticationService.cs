using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface IAuthenticationService
{
    public Task<AuthResult> SignInAsync(SignInModel signInData);

    public Task<AuthResult> SignUpAsync(SignUpModel signUpData);
}