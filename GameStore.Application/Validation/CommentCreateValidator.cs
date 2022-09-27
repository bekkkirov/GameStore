using FluentValidation;
using GameStore.Application.Models;

namespace GameStore.Application.Validation;

public class CommentCreateValidator : AbstractValidator<CommentCreateModel>
{
    public CommentCreateValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty()
            .WithMessage("Comment can't be empty.")
            .MaximumLength(250)
            .WithMessage("Comment length can't exceed 250 characters.");
    }
}