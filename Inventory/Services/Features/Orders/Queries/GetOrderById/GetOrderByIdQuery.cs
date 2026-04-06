using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderResponse>>;
