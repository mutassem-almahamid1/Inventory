using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;

namespace Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private new readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetBySupplierIdAsync(Guid supplierId, CancellationToken ct)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.SupplierId == supplierId)
            .ToListAsync(ct);
    }
}