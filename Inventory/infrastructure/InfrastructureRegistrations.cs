using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Persistence;
using Services.Abstractions.Security;

namespace Infrastructure;

public static class InfrastructureRegistrations
{
    public static void AddInventoryDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IJwtTokenFactory, JwtTokenFactory>();
    }
}