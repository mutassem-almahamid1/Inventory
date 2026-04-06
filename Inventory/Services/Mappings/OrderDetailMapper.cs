using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class OrderDetailMapper
{
    public static OrderDetail ToEntity(CreateOrderDetailRequest request)
    {
        return new OrderDetail
        {
            Id = Guid.NewGuid(),
            Quantity = request.Quantity,
            TotalPrice = request.TotalPrice,
            OrderId = request.OrderId,
            ProductId = request.ProductId,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static OrderDetail ToEntity(UpdateOrderDetailRequest request, OrderDetail existingOrderDetail)
    {
        if (request.Quantity.HasValue)
            existingOrderDetail.Quantity = request.Quantity.Value;

        if (request.TotalPrice.HasValue)
            existingOrderDetail.TotalPrice = request.TotalPrice.Value;

        if (request.OrderId.HasValue)
            existingOrderDetail.OrderId = request.OrderId.Value;

        if (request.ProductId.HasValue)
            existingOrderDetail.ProductId = request.ProductId.Value;

        existingOrderDetail.ModifiedOn = DateTimeOffset.UtcNow;

        return existingOrderDetail;
    }

    public static OrderDetailResponse ToResponse(OrderDetail orderDetail)
    {
        return new OrderDetailResponse
        {
            Id = orderDetail.Id,
            Quantity = orderDetail.Quantity,
            TotalPrice = orderDetail.TotalPrice,
            OrderId = orderDetail.OrderId,
            ProductId = orderDetail.ProductId,
            CreatedOn = orderDetail.CreatedOn
        };
    }
}
