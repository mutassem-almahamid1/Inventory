using FluentValidation;

namespace Services.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Request.UserNameOrEmail)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}