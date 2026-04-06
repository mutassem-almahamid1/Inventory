using Core.Common;
using MediatR;

namespace Services.Features.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken, string? IpAddress) : IRequest<Result>;