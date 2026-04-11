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
            options.UseNpgsql(appOptions.DefaultConnection);
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