using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => Array.Empty<string>();
    public string[] CacheKeyPrefixes => new[] { "Products-" };
}
