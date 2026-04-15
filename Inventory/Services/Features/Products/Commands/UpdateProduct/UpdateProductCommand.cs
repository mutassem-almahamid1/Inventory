using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, UpdateProductRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Product-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Products-" };
}
