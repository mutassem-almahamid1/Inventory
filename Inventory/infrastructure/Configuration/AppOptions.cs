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

  
    public bool ShowDetailedErrors { get; init; } = false;
}