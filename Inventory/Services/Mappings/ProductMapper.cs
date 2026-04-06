using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class ProductMapper
{
    public static Product ToEntity(CreateProductRequest request)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            UnitPrice = request.UnitPrice,
            Weight = request.Weight,
            Length = request.Length,
            Width = request.Width,
            Height = request.Height,
            TaxCost = request.TaxCost,
            ProfitPerUnit = request.ProfitPerUnit,
            ProductionCost = request.ProductionCost,
            CategoryId = request.CategoryId,
            InventoryId = request.InventoryId,
            TransactionId = request.TransactionId,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static Product ToEntity(UpdateProductRequest request, Product existingProduct)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            existingProduct.Name = request.Name;

        if (request.Description != null)
            existingProduct.Description = request.Description;

        if (request.UnitPrice.HasValue)
            existingProduct.UnitPrice = request.UnitPrice.Value;

        if (request.Weight.HasValue)
            existingProduct.Weight = request.Weight.Value;

        if (request.Length.HasValue)
            existingProduct.Length = request.Length.Value;

        if (request.Width.HasValue)
            existingProduct.Width = request.Width.Value;

        if (request.Height.HasValue)
            existingProduct.Height = request.Height.Value;

        if (request.TaxCost.HasValue)
            existingProduct.TaxCost = request.TaxCost.Value;

        if (request.ProfitPerUnit.HasValue)
            existingProduct.ProfitPerUnit = request.ProfitPerUnit.Value;

        if (request.ProductionCost.HasValue)
            existingProduct.ProductionCost = request.ProductionCost.Value;

        if (request.CategoryId.HasValue)
            existingProduct.CategoryId = request.CategoryId.Value;

        if (request.InventoryId.HasValue)
            existingProduct.InventoryId = request.InventoryId.Value;

        if (request.TransactionId.HasValue)
            existingProduct.TransactionId = request.TransactionId.Value;

        existingProduct.ModifiedOn = DateTimeOffset.UtcNow;

        return existingProduct;
    }

    public static ProductResponse ToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            Weight = product.Weight,
            Length = product.Length,
            Width = product.Width,
            Height = product.Height,
            TaxCost = product.TaxCost,
            ProfitPerUnit = product.ProfitPerUnit,
            ProductionCost = product.ProductionCost,
            CategoryId = product.CategoryId,
            InventoryId = product.InventoryId,
            TransactionId = product.TransactionId,
            CreatedOn = product.CreatedOn
        };
    }

    public static ProductResponse ToResponseWithDetails(Product product)
    {
        var response = ToResponse(product);
        response.CategoryName = product.Category?.Name;
        response.InventoryName = product.Inventory?.Name;
        return response;
    }
}
