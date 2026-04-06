using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;

namespace Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private new readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken ct)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Where(t => t.EmployeeId == employeeId)
            .ToListAsync(ct);
    }
}