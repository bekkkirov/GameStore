using FluentValidation;
using GameStore.Application.Models;
using GameStore.Domain.Entities;

namespace GameStore.Application.Validation;

public class GameCreateValidator : AbstractValidator<GameCreateModel>
{
    public GameCreateValidator()
    {
        RuleFor(g => g.Key)
            .NotEmpty()
            .WithMessage("Game key is required.")
            .MaximumLength(50)
            .WithMessage("Key length can't exceed 50 characters.");

        RuleFor(g => g.Name)
            .NotEmpty()
            .WithMessage("Game name is required.")
            .MaximumLength(50)
            .WithMessage("Name length can't exceed 50 characters.");

        RuleFor(g => g.Description)
            .NotEmpty()
            .WithMessage("Game description is required.")
            .MinimumLength(20)
            .WithMessage("Description must be at least 20 characters long.")
            .MaximumLength(500)
            .WithMessage("Description length can't exceed 500 characters.");

        RuleFor(g => g.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal zero.");
    }
}