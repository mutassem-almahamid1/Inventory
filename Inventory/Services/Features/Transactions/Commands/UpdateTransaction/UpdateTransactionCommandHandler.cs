using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTransactionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var existingTransaction = await unitOfWork.Transactions.GetByIdAsync(request.Id, cancellationToken);
        if (existingTransaction is null)
            return Result.Failure<Guid>($"Transaction with ID {request.Id} not found.");

        TransactionMapper.ToEntity(request.Request, existingTransaction);
        unitOfWork.Transactions.Update(existingTransaction);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingTransaction.Id);
    }
}
