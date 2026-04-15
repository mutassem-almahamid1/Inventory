namespace Core.Interfaces;

/// <summary>
/// Marker interface for queries that can be cached
/// </summary>
public interface ICacheableQuery
{
    /// <summary>
    /// Unique cache key for this query
    /// </summary>
    string CacheKey { get; }

    /// <summary>
    /// Cache expiration time in seconds (default: 5 minutes)
    /// </summary>
    int ExpirationInSeconds { get; }

    /// <summary>
    /// Whether to bypass cache (default: false)
    /// </summary>
    bool BypassCache { get; }
}

/// <summary>
/// Default implementation with common defaults
/// </summary>
public abstract class CacheableQueryBase : ICacheableQuery
{
    public abstract string CacheKey { get; }
    
    public virtual int ExpirationInSeconds => 300; // 5 minutes
    
    public virtual bool BypassCache => false;
}