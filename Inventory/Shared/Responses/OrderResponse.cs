namespace Shared.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }
    public DateTimeOffset ExpectedDeliveryDate { get; set; }
    public int OrderType { get; set; }
    public DateTimeOffset ReceivedDate { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
