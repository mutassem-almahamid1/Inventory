using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByInventory;

public record GetProductsByInventoryQuery(Guid Id):IRequest<Result<List<ProductResponse>>>;
