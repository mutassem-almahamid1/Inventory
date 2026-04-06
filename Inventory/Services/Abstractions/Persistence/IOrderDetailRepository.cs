using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task<List<OrderDetail>> GetByOrderIdAsync(Guid orderId, CancellationToken ct);
}