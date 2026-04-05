using Core.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Persistence;
using Shared.Responses;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public virtual async ValueTask<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<PagedResponse<T>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToPagedResponseAsync(pageNumber, pageSize, cancellationToken: cancellationToken);
    }

    public virtual async ValueTask AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void Delete(Guid id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}