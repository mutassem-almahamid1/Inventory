using Core.Common;
using MediatR;
using Services.Abstractions.Security;
using Shared.Responses;

namespace Services.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IIdentityService identityService)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    public Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return identityService.RefreshAsync(
            request.Request.RefreshToken,
            request.IpAddress,
            request.UserAgent,
            cancellationToken);
    }
}