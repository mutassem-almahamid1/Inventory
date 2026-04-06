using Core.Common;
using MediatR;
using Shared.Requests;
using Shared.Responses;

namespace Services.Features.Auth.Commands.Signup;

public record SignupCommand(SignupRequest Request) : IRequest<Result<AuthResponse>>;