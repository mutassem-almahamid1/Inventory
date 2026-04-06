using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<List<Order>> GetBySupplierIdAsync(Guid supplierId, CancellationToken ct);
}