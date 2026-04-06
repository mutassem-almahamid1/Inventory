using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetTopProfitableProducts;

public record GetTopProfitableProductsQuery(int Count) : IRequest<Result<List<ProductResponse>>>;
