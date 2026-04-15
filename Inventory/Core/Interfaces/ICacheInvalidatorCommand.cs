namespace Core.Interfaces;

/// <summary>
/// Marker interface for commands that should invalidate cache after execution
/// </summary>
public interface ICacheInvalidatorCommand
{
    /// <summary>
    /// Exact cache keys to invalidate after command execution
    /// </summary>
    string[] CacheKeys { get; }

    /// <summary>
    /// Cache key prefixes to invalidate after command execution
    /// Use this for invalidating all related keys (e.g., pagination pages, related entities)
    /// </summary>
    string[] CacheKeyPrefixes { get; }
}

/// <summary>
/// Base class for commands that need cache invalidation
/// </summary>
public abstract class CacheInvalidatorCommandBase : ICacheInvalidatorCommand
{
    public abstract string[] CacheKeys { get; }
    
    public virtual string[] CacheKeyPrefixes => Array.Empty<string>();
}