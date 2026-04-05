namespace Core.Entities;

public class Inventory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public string Location { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}