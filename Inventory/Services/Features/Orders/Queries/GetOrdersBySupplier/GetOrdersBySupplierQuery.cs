using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrdersBySupplier;

public record GetOrdersBySupplierQuery(Guid SupplierId) : IRequest<Result<List<OrderResponse>>>;
