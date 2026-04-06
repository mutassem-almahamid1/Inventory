using Core.Common;
using MediatR;
using Services.Abstractions.Security;
using Shared.Responses;

namespace Services.Features.Auth.Commands.Signup;

public class SignupCommandHandler(IIdentityService identityService)
    : IRequestHandler<SignupCommand, Result<AuthResponse>>
{
    public Task<Result<AuthResponse>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        return identityService.SignupAsync(
            request.Request.UserName,
            request.Request.FullName,
            request.Request.Email,
            request.Request.Password,
            request.Request.Role,
            cancellationToken);
    }
}