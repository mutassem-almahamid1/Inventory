using Core.Common;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Security;
using Shared.Responses;

namespace Infrastructure.Security;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtTokenFactory tokenFactory,
    AppDbContext dbContext) : IIdentityService
{
    public async Task<Result<AuthResponse>> SignupAsync(
        string userName,
        string fullName,
        string email,
        string password,
        string role,
        CancellationToken cancellationToken)
    {
        var normalizedEmail = email.Trim().ToUpperInvariant();
        var normalizedUserName = userName.Trim().ToUpperInvariant();

        var emailExists =
            await userManager.Users.AnyAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        if (emailExists)
            return Result.Failure<AuthResponse>("Email is already registered.");

        var userNameExists =
            await userManager.Users.AnyAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        if (userNameExists)
            return Result.Failure<AuthResponse>("Username is already taken.");

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = userName.Trim(),
            FullName = fullName.Trim(),
            Email = email.Trim(),
            IsActive = true,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return Result.Failure<AuthResponse>(string.Join("; ", createResult.Errors.Select(x => x.Description)));

        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
            return Result.Failure<AuthResponse>(string.Join("; ", roleResult.Errors.Select(x => x.Description)));

        var tokens = tokenFactory.CreateTokenPair(user, role);
        await PersistRefreshTokenAsync(user.Id, tokens, null, null, cancellationToken);

        var response = new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Role = role,
            Tokens = tokens
        };

        return Result.Success(response);
    }

    public async Task<Result<AuthResponse>> LoginAsync(string userNameOrEmail, string password,
        CancellationToken cancellationToken)
    {
        var normalized = userNameOrEmail.Trim().ToUpperInvariant();

        var user = await userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedUserName == normalized || u.NormalizedEmail == normalized,
                cancellationToken);

        if (user is null)
            return Result.Failure<AuthResponse>("Invalid username/email or password.");

        if (!user.IsActive)
            return Result.Failure<AuthResponse>("User account is inactive.");

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, true);
        if (!signInResult.Succeeded)
            return Result.Failure<AuthResponse>("Invalid username/email or password.");

        var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(role))
            return Result.Failure<AuthResponse>("User has no assigned role.");

        var tokens = tokenFactory.CreateTokenPair(user, role);
        await PersistRefreshTokenAsync(user.Id, tokens, null, null, cancellationToken);

        var response = new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Role = role,
            Tokens = tokens
        };

        return Result.Success(response);
    }

    public async Task<Result<TokenResponse>> RefreshAsync(string refreshToken, string? ipAddress, string? userAgent,
        CancellationToken cancellationToken)
    {
        var tokenHash = tokenFactory.HashRefreshToken(refreshToken);
        var existingToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

        if (existingToken is null)
            return Result.Failure<TokenResponse>("Invalid refresh token.");

        if (!existingToken.IsActive)
            return Result.Failure<TokenResponse>("Refresh token is no longer active.");

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == existingToken.UserId, cancellationToken);
        if (user is null || !user.IsActive)
            return Result.Failure<TokenResponse>("User not found or inactive.");

        var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(role))
            return Result.Failure<TokenResponse>("User has no assigned role.");

        var tokens = tokenFactory.CreateTokenPair(user, role);

        existingToken.RevokedAt = DateTimeOffset.UtcNow;
        existingToken.RevokedByIp = ipAddress;
        existingToken.ReplacedByTokenHash = tokenFactory.HashRefreshToken(tokens.RefreshToken);

        await PersistRefreshTokenAsync(user.Id, tokens, ipAddress, userAgent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(tokens);
    }

    public async Task<Result> LogoutAsync(string refreshToken, string? ipAddress, CancellationToken cancellationToken)
    {
        var tokenHash = tokenFactory.HashRefreshToken(refreshToken);
        var existingToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

        if (existingToken is null)
            return Result.Failure("Invalid refresh token.");

        if (existingToken.RevokedAt is not null)
            return Result.Success();

        existingToken.RevokedAt = DateTimeOffset.UtcNow;
        existingToken.RevokedByIp = ipAddress;

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<AuthResponse>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user is null)
            return Result.Failure<AuthResponse>("User not found.");

        var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;

        return Result.Success(new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Role = role,
            Tokens = new TokenResponse()
        });
    }

    private async Task PersistRefreshTokenAsync(
        Guid userId,
        TokenResponse tokens,
        string? ipAddress,
        string? userAgent,
        CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenFactory.HashRefreshToken(tokens.RefreshToken),
            CreatedAt = DateTimeOffset.UtcNow,
            ExpiresAt = tokens.RefreshTokenExpiresAt,
            CreatedByIp = ipAddress,
            UserAgent = userAgent
        };

        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}