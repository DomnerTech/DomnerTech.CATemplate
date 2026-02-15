using FluentValidation;

namespace DomnerTech.CATemplate.Application.Features.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Username cannot be greater than 50 chars");
        RuleFor(x => x.Age)
            .Must(a => a > 0)
            .WithMessage("Age must be positive");
    }
}