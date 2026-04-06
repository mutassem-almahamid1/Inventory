using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class CategoryMapper
{
    public static Category ToEntity(CreateCategoryRequest request)
    {
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static Category ToEntity(UpdateCategoryRequest request, Category existingCategory)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            existingCategory.Name = request.Name;

        if (request.Description != null)
            existingCategory.Description = request.Description;

        existingCategory.ModifiedOn = DateTimeOffset.UtcNow;

        return existingCategory;
    }

    public static CategoryResponse ToResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ProductsCount = category.Products?.Count ?? 0,
            CreatedOn = category.CreatedOn
        };
    }

    public static CategoryResponse ToResponseWithCount(Category category)
    {
        var response = ToResponse(category);
        response.ProductsCount = category.Products?.Count ?? 0;
        return response;
    }
}