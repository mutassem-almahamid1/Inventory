namespace Core.Entities;

public class Transaction : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int Quantity { get; set; }
    public int TransactionType { get; set; }
    public string Notes { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}