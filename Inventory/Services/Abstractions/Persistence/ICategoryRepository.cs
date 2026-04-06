using Core.Entities;

namespace Services.Abstractions.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}