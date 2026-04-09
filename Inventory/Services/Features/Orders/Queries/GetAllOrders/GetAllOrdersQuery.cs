using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<OrderResponse>>>;
