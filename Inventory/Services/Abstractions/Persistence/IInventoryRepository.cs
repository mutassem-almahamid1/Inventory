using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface IInventoryRepository : IGenericRepository<Inventory>
{
    Task<Inventory?> GetByNameAsync(string name, CancellationToken cancellationToken);
}