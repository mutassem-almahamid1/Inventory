using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery(Guid UserId) : IRequest<Result<AuthResponse>>;