namespace Shared.Responses;

public class OrderDetailResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
