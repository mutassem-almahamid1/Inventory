using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Product-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Products-" };
}
