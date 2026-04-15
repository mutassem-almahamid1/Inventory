using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Abstractions.Persistence;
using Services.Abstractions.Security;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class InfrastructureRegistrations
{
    public static IServiceCollection AddInventoryDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration.GetSection(AppOptions.SectionName));

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(appOptions.DefaultConnection, op =>
            {
                // Set how long the database command should to run before throwing a timeout timeout exception.
                op.CommandTimeout(appOptions.CommandTimeout);

                // Enable automatic retry logic on transient errors (like temporary network drops).
                // MaxRetryCount: Maximum number of retry attempts.
                // MaxRetryDelay: Maximum time to wait between each retry attempt.
                // errorCodesToAdd: A list of specific Postgres error codes you want to retry on (null means standard transient errors).
                op.EnableRetryOnFailure(
                    maxRetryCount: appOptions.MaxRetryCount,
                    maxRetryDelay: TimeSpan.FromSeconds(appOptions.MaxRetryDelayInSeconds),
                    errorCodesToAdd: null);
            });

            if (appOptions.ShowDetailedErrors)
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IJwtTokenFactory, JwtTokenFactory>();
    }
}