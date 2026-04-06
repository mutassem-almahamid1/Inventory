namespace Shared.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProductsCount { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}