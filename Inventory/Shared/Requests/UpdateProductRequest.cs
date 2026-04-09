namespace Shared.Requests;

public class UpdateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? TaxCost { get; set; }
    public decimal? ProfitPerUnit { get; set; }
    public decimal? ProductionCost { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? InventoryId { get; set; }
    public Guid? TransactionId { get; set; }
}
