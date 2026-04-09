using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetAllOrderDetails;

public record GetAllOrderDetailsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<OrderDetailResponse>>>;
