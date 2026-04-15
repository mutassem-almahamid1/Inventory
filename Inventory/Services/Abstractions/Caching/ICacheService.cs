namespace Services.Abstractions.Caching;

/// <summary>
/// Cache service interface for managing in-memory cache
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Get cached value by key
    /// </summary>
    T? Get<T>(string key);

    /// <summary>
    /// Set cache value with expiration
    /// </summary>
    void Set<T>(string key, T value, TimeSpan expiration);

    /// <summary>
    /// Remove cached value by key
    /// </summary>
    void Remove(string key);

    /// <summary>
    /// Check if key exists in cache
    /// </summary>
    bool Exists(string key);

    /// <summary>
    /// Remove cached values by pattern (e.g., "products-*")
    /// </summary>
    void RemoveByPattern(string pattern);
}