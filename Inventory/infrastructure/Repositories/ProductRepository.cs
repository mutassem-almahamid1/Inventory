using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private new readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
    }

    
    public async Task<List<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Products.AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByInventoryAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking()
            .Where(p => p.InventoryId == requestId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetByPriceRangeAsync(decimal requestMinPrice, decimal requestMaxPrice, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking()
            .Where(p => p.UnitPrice >= requestMinPrice && p.UnitPrice <= requestMaxPrice)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetTopProfitableAsync(int count, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking()
            .OrderByDescending(p => p.ProfitPerUnit)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}