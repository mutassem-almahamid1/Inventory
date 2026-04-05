using Core.Entities;
using Shared.Responses;

namespace Services.Abstractions.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    ValueTask<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PagedResponse<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(Guid id);
}