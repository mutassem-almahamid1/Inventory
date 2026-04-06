using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailsByOrderId;

public record GetOrderDetailsByOrderIdQuery(Guid OrderId) : IRequest<Result<List<OrderDetailResponse>>>;
