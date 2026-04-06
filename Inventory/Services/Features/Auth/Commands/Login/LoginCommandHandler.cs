using Core.Common;
using MediatR;
using Services.Abstractions.Security;
using Shared.Responses;

namespace Services.Features.Auth.Commands.Login;

public class LoginCommandHandler(IIdentityService identityService)
    : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    public Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return identityService.LoginAsync(request.Request.UserNameOrEmail, request.Request.Password, cancellationToken);
    }
}