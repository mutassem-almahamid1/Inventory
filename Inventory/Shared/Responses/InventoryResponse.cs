namespace Shared.Responses;

public class InventoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTimeOffset LastUpdated { get; set; }
}