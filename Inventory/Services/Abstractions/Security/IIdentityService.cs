using Core.Common;
using Shared.Responses;

namespace Services.Abstractions.Security;

public interface IIdentityService
{
    Task<Result<AuthResponse>> SignupAsync(
        string userName,
        string fullName,
        string email,
        string password,
        string role,
        CancellationToken cancellationToken);

    Task<Result<AuthResponse>> LoginAsync(string userNameOrEmail, string password, CancellationToken cancellationToken);

    Task<Result<TokenResponse>> RefreshAsync(string refreshToken, string? ipAddress, string? userAgent,
        CancellationToken cancellationToken);

    Task<Result> LogoutAsync(string refreshToken, string? ipAddress, CancellationToken cancellationToken);

    Task<Result<AuthResponse>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken);
}