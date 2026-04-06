using Core.Common;
using MediatR;
using Services.Abstractions.Security;

namespace Services.Features.Auth.Commands.Logout;

public class LogoutCommandHandler(IIdentityService identityService)
    : IRequestHandler<LogoutCommand, Result>
{
    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return identityService.LogoutAsync(request.RefreshToken, request.IpAddress, cancellationToken);
    }
}