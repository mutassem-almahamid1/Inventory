using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<ProductResponse>>>;
