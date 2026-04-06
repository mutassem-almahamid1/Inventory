namespace Core.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } // The unique identifier for the refresh token

    public Guid UserId { get; set; } // The ID of the user who owns this token

    public string TokenHash { get; set; } = string.Empty; // The securely hashed value of the token

    public DateTimeOffset ExpiresAt { get; set; } // The exact date and time the token expires
    public DateTimeOffset CreatedAt { get; set; } // The exact date and time the token was generated
    public DateTimeOffset? RevokedAt { get; set; } // When the token was cancelled or invalidated

    public string? ReplacedByTokenHash { get; set; } // The hash of the new token that replaced this one
    public string? CreatedByIp { get; set; } // The IP address from which the token was requested
    public string? RevokedByIp { get; set; } // The IP address from which the token was revoked
    public string? UserAgent { get; set; } // Information about the user's browser or device

    public bool IsActive => RevokedAt is null && ExpiresAt > DateTimeOffset.UtcNow; // Checks if the token is still valid and not revoked
}