namespace Services.Abstractions.Persistence;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IInventoryRepository Inventories { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    ITransactionRepository Transactions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    
  
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
    
  
    Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);
}