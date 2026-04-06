using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductByName;

public record GetProductByNameQuery(string Name) : IRequest<Result<ProductResponse>>;
