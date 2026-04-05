namespace Core.Entities;

public class OrderDetail : BaseEntity
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new();

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}