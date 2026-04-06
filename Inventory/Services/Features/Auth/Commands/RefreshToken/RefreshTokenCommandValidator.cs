using FluentValidation;

namespace Services.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Request.RefreshToken)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}