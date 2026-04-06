using FluentValidation;

namespace Services.Features.Transactions.Queries.GetTransactionsByEmployee;

public class GetTransactionsByEmployeeQueryValidator : AbstractValidator<GetTransactionsByEmployeeQuery>
{
    public GetTransactionsByEmployeeQueryValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
