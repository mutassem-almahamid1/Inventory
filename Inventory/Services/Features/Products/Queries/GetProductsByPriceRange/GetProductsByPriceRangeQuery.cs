using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByPriceRange;

public record GetProductsByPriceRangeQuery(decimal MinPrice, decimal MaxPrice): IRequest<Result<List<ProductResponse>>>;
