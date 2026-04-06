using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class OrderMapper
{
    public static Order ToEntity(CreateOrderRequest request)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            SupplierId = request.SupplierId,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
            ExpectedDeliveryDate = request.ExpectedDeliveryDate,
            OrderType = request.OrderType,
            ReceivedDate = request.ReceivedDate,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static Order ToEntity(UpdateOrderRequest request, Order existingOrder)
    {
        if (request.SupplierId.HasValue)
            existingOrder.SupplierId = request.SupplierId.Value;

        if (request.OrderDate.HasValue)
            existingOrder.OrderDate = request.OrderDate.Value;

        if (request.TotalAmount.HasValue)
            existingOrder.TotalAmount = request.TotalAmount.Value;

        if (request.Status.HasValue)
            existingOrder.Status = request.Status.Value;

        if (request.ExpectedDeliveryDate.HasValue)
            existingOrder.ExpectedDeliveryDate = request.ExpectedDeliveryDate.Value;

        if (request.OrderType.HasValue)
            existingOrder.OrderType = request.OrderType.Value;

        if (request.ReceivedDate.HasValue)
            existingOrder.ReceivedDate = request.ReceivedDate.Value;

        existingOrder.ModifiedOn = DateTimeOffset.UtcNow;

        return existingOrder;
    }

    public static OrderResponse ToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            SupplierId = order.SupplierId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            ExpectedDeliveryDate = order.ExpectedDeliveryDate,
            OrderType = order.OrderType,
            ReceivedDate = order.ReceivedDate,
            CreatedOn = order.CreatedOn
        };
    }
}
