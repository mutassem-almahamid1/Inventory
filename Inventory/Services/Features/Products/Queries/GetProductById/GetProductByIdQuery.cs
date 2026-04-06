using MediatR;
using Core.Common;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductResponse>>;
