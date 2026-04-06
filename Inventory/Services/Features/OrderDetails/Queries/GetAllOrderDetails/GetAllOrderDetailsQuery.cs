using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetAllOrderDetails;

public record GetAllOrderDetailsQuery : IRequest<Result<List<OrderDetailResponse>>>;
