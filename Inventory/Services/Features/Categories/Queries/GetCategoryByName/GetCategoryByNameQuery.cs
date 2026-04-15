using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetCategoryByName;

public record GetCategoryByNameQuery(string Name) : IRequest<Result<CategoryResponse>>, ICacheableQuery
{
    public string CacheKey => $"Category-Name-{Name}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
