using Core.Common;
using MediatR;

namespace Services.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result<Guid>>;
