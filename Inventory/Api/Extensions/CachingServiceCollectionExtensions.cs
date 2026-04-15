using Infrastructure.Caching;
using MediatR;
using Services.Abstractions.Caching;
using Services.Behaviors;

namespace Api.Extensions;

/// <summary>
/// Extension methods for registering caching services
/// </summary>
public static class CachingServiceCollectionExtensions
{
    /// <summary>
    /// Add caching services to the service collection
    /// </summary>
    public static IServiceCollection AddCachingServices(this IServiceCollection services)
    {
        // Add IMemoryCache
        services.AddMemoryCache();

        // Add CacheService
        services.AddSingleton<ICacheService, CacheService>();

        // Add CachingBehavior as pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        // Add CacheInvalidationBehavior as pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));

        return services;
    }
}