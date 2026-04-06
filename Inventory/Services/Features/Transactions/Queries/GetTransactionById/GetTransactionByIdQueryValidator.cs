using FluentValidation;

namespace Services.Features.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
{
    public GetTransactionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
