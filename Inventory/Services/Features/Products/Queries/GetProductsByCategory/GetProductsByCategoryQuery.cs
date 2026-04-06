using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<Result<List<ProductResponse>>>;

