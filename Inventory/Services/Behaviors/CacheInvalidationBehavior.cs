using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Abstractions.Caching;

namespace Services.Behaviors;

/// <summary>
/// Pipeline behavior for invalidating cache after command execution
/// </summary>
public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ICacheService _cache;
    private readonly ILogger<CacheInvalidationBehavior<TRequest, TResponse>> _logger;

    public CacheInvalidationBehavior(ICacheService cache, ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Execute the command
        var response = await next();

        // Only invalidate cache for ICacheInvalidatorCommand
        if (request is ICacheInvalidatorCommand cacheInvalidator)
        {
            // Remove exact keys
            foreach (var cacheKey in cacheInvalidator.CacheKeys)
            {
                _cache.Remove(cacheKey);
                _logger.LogDebug("Invalidated cache for key: {CacheKey}", cacheKey);
            }

            // Remove by prefixes (e.g., "Products-" will invalidate "Products-All-1-10", "Products-All-2-10", etc.)
            foreach (var prefix in cacheInvalidator.CacheKeyPrefixes)
            {
                _cache.RemoveByPattern(prefix);
                _logger.LogDebug("Invalidated cache for prefix: {Prefix}", prefix);
            }
        }

        return response;
    }
}