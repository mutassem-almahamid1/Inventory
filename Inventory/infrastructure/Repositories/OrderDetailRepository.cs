using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;

namespace Infrastructure.Repositories;

public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    private new readonly AppDbContext _context;

    public OrderDetailRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OrderDetail>> GetByOrderIdAsync(Guid orderId, CancellationToken ct)
    {
        return await _context.OrderDetails
            .AsNoTracking()
            .Where(od => od.OrderId == orderId)
            .ToListAsync(ct);
    }
}