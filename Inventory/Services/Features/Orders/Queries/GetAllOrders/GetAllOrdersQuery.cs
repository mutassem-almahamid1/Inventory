using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<Result<List<OrderResponse>>>;
