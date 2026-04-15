using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Category-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Categories-" };
}
