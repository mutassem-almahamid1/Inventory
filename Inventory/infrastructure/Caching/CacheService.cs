using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Services.Abstractions.Caching;

namespace Infrastructure.Caching;

/// <summary>
/// In-memory cache service implementation using IMemoryCache with prefix-based invalidation support
/// </summary>
public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CacheService> _logger;
    private readonly ConcurrentDictionary<string, string> _cacheKeys = new();

    public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public T? Get<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            _logger.LogWarning("Attempted to get cache with empty key");
            return default;
        }

        if (_cache.TryGetValue(key, out T? value))
        {
            _logger.LogDebug("Cache hit for key: {CacheKey}", key);
            return value;
        }

        _logger.LogDebug("Cache miss for key: {CacheKey}", key);
        return default;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        if (string.IsNullOrEmpty(key))
        {
            _logger.LogWarning("Attempted to set cache with empty key");
            return;
        }

        if (value == null)
        {
            _logger.LogWarning("Attempted to cache null value for key: {CacheKey}", key);
            return;
        }

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(expiration)
            .SetSlidingExpiration(TimeSpan.FromMinutes(2))
            .SetPriority(CacheItemPriority.Normal)
            .RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                // Remove from tracking dictionary when evicted
                if (key != null)
                {
                    _cacheKeys.TryRemove(key.ToString()!, out _);
                }
            });

        _cache.Set(key, value, cacheEntryOptions);
        
        // Track the key for prefix-based invalidation
        _cacheKeys.TryAdd(key, key);
        
        _logger.LogDebug("Cached value for key: {CacheKey}, expiration: {Expiration}", key, expiration);
    }

    public void Remove(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            _logger.LogWarning("Attempted to remove cache with empty key");
            return;
        }

        _cache.Remove(key);
        _cacheKeys.TryRemove(key, out _);
        _logger.LogDebug("Removed cache for key: {CacheKey}", key);
    }

    public bool Exists(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        return _cache.TryGetValue(key, out _);
    }

    public void RemoveByPattern(string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            _logger.LogWarning("Attempted to remove cache with empty pattern");
            return;
        }

        // Remove all keys that start with the pattern
        var keysToRemove = _cacheKeys.Keys
            .Where(key => key.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
            _logger.LogDebug("Removed cache for key: {CacheKey} (pattern: {Pattern})", key, pattern);
        }

        if (keysToRemove.Count > 0)
        {
            _logger.LogInformation("Invalidated {Count} cache entries with pattern: {Pattern}", keysToRemove.Count, pattern);
        }
    }
}