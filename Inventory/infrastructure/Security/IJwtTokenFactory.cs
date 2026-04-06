using Infrastructure.Identity;
using Shared.Responses;

namespace Infrastructure.Security;

public interface IJwtTokenFactory
{
    TokenResponse CreateTokenPair(ApplicationUser user, string role);
    string HashRefreshToken(string refreshToken);
}