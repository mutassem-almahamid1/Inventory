namespace Shared.Responses;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int Quantity { get; set; }
    public int TransactionType { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTimeOffset CreatedOn { get; set; }
}
