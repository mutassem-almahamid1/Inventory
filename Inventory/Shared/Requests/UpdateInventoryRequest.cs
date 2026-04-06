namespace Shared.Requests;

public class UpdateInventoryRequest
{
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public int? ReorderLevel { get; set; }
    public string? Location { get; set; }
}
