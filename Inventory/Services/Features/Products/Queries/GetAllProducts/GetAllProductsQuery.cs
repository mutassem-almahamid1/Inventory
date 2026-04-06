using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<Result<List<ProductResponse>>>;
