using FluentValidation;
using GameStore.Application.Models;

namespace GameStore.Application.Validation;

public class SignUpValidator : AbstractValidator<SignUpModel>
{
    public SignUpValidator()
    {
        RuleFor(su => su.UserName)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(5)
            .WithMessage("Username must be at least 5 characters long.")
            .MaximumLength(20)
            .WithMessage("Username length can't exceed 20 characters.");

        RuleFor(su => su.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is invalid.")
            .MaximumLength(50)
            .WithMessage("Email length can't exceed 50 characters.");

        RuleFor(su => su.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(30)
            .WithMessage("First name length can't exceed 30 characters.");

        RuleFor(su => su.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(2)
            .WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(30)
            .WithMessage("Last name length can't exceed 30 characters.");

        RuleFor(su => su.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(5)
            .WithMessage("Password must be at least 5 characters long.")
            .MaximumLength(20)
            .WithMessage("Password length can't exceed 20 characters.");
    }
}