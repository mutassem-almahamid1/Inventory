namespace Shared.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public float Weight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public decimal TaxCost { get; set; }
    public decimal ProfitPerUnit { get; set; }
    public decimal ProductionCost { get; set; }
    public Guid CategoryId { get; set; }
    public Guid InventoryId { get; set; }
    public Guid TransactionId { get; set; }
}