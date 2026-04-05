namespace Core.Entities;

public class Order : BaseEntity
{
    public Guid SupplierId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }
    public DateTimeOffset ExpectedDeliveryDate { get; set; }
    public int OrderType { get; set; }
    public DateTimeOffset ReceivedDate { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new();
}
