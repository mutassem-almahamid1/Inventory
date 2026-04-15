using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Abstractions.Caching;

namespace Services.Behaviors;

/// <summary>
/// Pipeline behavior for caching query responses
/// </summary>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ICacheService _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(ICacheService cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only cache ICacheableQuery requests
        if (request is not ICacheableQuery cacheableRequest)
        {
            return await next();
        }

        // Bypass cache if requested
        if (cacheableRequest.BypassCache)
        {
            _logger.LogDebug("Cache bypassed for key: {CacheKey}", cacheableRequest.CacheKey);
            return await next();
        }

        // Try to get from cache
        var cachedValue = _cache.Get<TResponse>(cacheableRequest.CacheKey);
        if (cachedValue != null)
        {
            _logger.LogDebug("Returning cached response for key: {CacheKey}", cacheableRequest.CacheKey);
            return cachedValue;
        }

        // Execute query and cache result
        _logger.LogDebug("Executing query and caching result for key: {CacheKey}", cacheableRequest.CacheKey);
        var response = await next();

        if (response != null)
        {
            var expiration = TimeSpan.FromSeconds(cacheableRequest.ExpirationInSeconds);
            _cache.Set(cacheableRequest.CacheKey, response, expiration);
            _logger.LogDebug("Cached response for key: {CacheKey}, expiration: {Expiration} seconds", 
                cacheableRequest.CacheKey, cacheableRequest.ExpirationInSeconds);
        }

        return response;
    }
}