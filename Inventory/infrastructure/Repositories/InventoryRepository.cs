using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;

namespace Infrastructure.Repositories;

public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext context) : base(context)
    {
    }

    public Task<Inventory?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _context.Set<Inventory>()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}