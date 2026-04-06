using Core.Common;
using MediatR;
using Shared.Requests;
using Shared.Responses;

namespace Services.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(
    RefreshTokenRequest Request,
    string? IpAddress,
    string? UserAgent) : IRequest<Result<TokenResponse>>;