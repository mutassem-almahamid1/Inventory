using Core.Common;
using MediatR;
using Shared.Requests;
using Shared.Responses;

namespace Services.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequest Request) : IRequest<Result<AuthResponse>>;