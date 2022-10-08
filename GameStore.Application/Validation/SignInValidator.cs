using FluentValidation;
using GameStore.Application.Models;

namespace GameStore.Application.Validation;

public class SignInValidator : AbstractValidator<SignInModel>
{
    public SignInValidator()
    {
        RuleFor(si => si.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(si => si.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}