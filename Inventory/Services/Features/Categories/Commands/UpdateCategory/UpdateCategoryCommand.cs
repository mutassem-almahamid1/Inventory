using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, UpdateCategoryRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Category-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Categories-" };
}
