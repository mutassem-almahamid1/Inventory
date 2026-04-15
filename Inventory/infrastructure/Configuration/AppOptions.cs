using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configuration;


public sealed class AppOptions
{
    public const string SectionName = "App";

    /// <summary>
    /// Database connection string.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; init; } = string.Empty;

  
    [Range(1, 300)]
    public int CommandTimeout { get; init; } = 30;

    /// <summary>
    /// The maximum number of retry attempts if a transient failure occurs when connecting to the database.
    /// Example: 5 means it will try 5 times before giving up.
    /// </summary>
    [Range(1, 10)]
    public int MaxRetryCount { get; init; } = 5;

    /// <summary>
    /// The maximum delay in seconds between connection retries.
    /// Example: 30 means it won't wait longer than 30 seconds between any two retry attempts.
    /// </summary>
    [Range(1, 60)]
    public int MaxRetryDelayInSeconds { get; init; } = 30;

    public bool ShowDetailedErrors { get; init; } = false;
}