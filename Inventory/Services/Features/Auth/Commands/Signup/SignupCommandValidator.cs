using FluentValidation;
using Services.Abstractions.Security;

namespace Services.Features.Auth.Commands.Signup;

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator()
    {
        RuleFor(x => x.Request.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(x => x.Request.FullName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} is invalid.")
            .MaximumLength(256).WithMessage("{PropertyName} must not exceed 256 characters.");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters.");

        RuleFor(x => x.Request.Role)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(role => SecurityRoles.All.Contains(role))
            .WithMessage($"Role must be one of: {string.Join(", ", SecurityRoles.All)}.");
    }
}