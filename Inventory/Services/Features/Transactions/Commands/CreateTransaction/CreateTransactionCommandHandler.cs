using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTransactionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = TransactionMapper.ToEntity(request.Request);

        await unitOfWork.Transactions.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(transaction.Id);
    }
}
