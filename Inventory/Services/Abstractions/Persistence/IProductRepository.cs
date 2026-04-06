using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetByNameAsync(string name);
    Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetProductsByInventoryAsync(Guid requestId, CancellationToken cancellationToken);
    Task<List<Product>> GetByPriceRangeAsync(decimal requestMinPrice, decimal requestMaxPrice, CancellationToken cancellationToken);
    Task<List<Product>> GetTopProfitableAsync(int count, CancellationToken cancellationToken);
}