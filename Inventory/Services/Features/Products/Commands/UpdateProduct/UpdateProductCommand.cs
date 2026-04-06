using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id,UpdateProductRequest Request) : IRequest<Result<Guid>>;
