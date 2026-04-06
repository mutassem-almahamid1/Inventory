using Core.Common;
using MediatR;
using Services.Abstractions.Security;
using Shared.Responses;

namespace Services.Features.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IIdentityService identityService)
    : IRequestHandler<GetCurrentUserQuery, Result<AuthResponse>>
{
    public Task<Result<AuthResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return identityService.GetCurrentUserAsync(request.UserId, cancellationToken);
    }
}