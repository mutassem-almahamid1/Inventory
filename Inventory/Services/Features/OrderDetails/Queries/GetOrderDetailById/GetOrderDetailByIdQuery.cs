using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailById;

public record GetOrderDetailByIdQuery(Guid Id) : IRequest<Result<OrderDetailResponse>>;
