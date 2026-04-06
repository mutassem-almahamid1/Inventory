using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<List<Transaction>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken ct);
}