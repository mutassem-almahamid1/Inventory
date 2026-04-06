using FluentValidation;

namespace Services.Features.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
{
    public GetCurrentUserQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}