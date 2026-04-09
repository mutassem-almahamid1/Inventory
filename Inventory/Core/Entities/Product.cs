namespace Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal TaxCost { get; set; }
    public decimal ProfitPerUnit { get; set; }
    public decimal ProductionCost { get; set; }

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid? InventoryId { get; set; }
    public Inventory? Inventory { get; set; }

    public Guid? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new();
}