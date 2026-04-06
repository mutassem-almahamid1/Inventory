using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class TransactionMapper
{
    public static Transaction ToEntity(CreateTransactionRequest request)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            EmployeeId = request.EmployeeId,
            TransactionDate = request.TransactionDate,
            Quantity = request.Quantity,
            TransactionType = request.TransactionType,
            Notes = request.Notes,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static Transaction ToEntity(UpdateTransactionRequest request, Transaction existingTransaction)
    {
        if (request.EmployeeId.HasValue)
            existingTransaction.EmployeeId = request.EmployeeId.Value;

        if (request.TransactionDate.HasValue)
            existingTransaction.TransactionDate = request.TransactionDate.Value;

        if (request.Quantity.HasValue)
            existingTransaction.Quantity = request.Quantity.Value;

        if (request.TransactionType.HasValue)
            existingTransaction.TransactionType = request.TransactionType.Value;

        if (request.Notes is not null)
            existingTransaction.Notes = request.Notes;

        existingTransaction.ModifiedOn = DateTimeOffset.UtcNow;

        return existingTransaction;
    }

    public static TransactionResponse ToResponse(Transaction transaction)
    {
        return new TransactionResponse
        {
            Id = transaction.Id,
            EmployeeId = transaction.EmployeeId,
            TransactionDate = transaction.TransactionDate,
            Quantity = transaction.Quantity,
            TransactionType = transaction.TransactionType,
            Notes = transaction.Notes,
            CreatedOn = transaction.CreatedOn
        };
    }
}
