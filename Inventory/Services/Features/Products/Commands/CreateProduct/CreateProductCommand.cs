using MediatR;
using Core.Common;
using Shared.Requests;

namespace Services.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<Result<Guid>>;
