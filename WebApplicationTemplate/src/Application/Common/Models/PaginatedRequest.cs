namespace Application.Common.Models;

public class PaginatedRequest
{
    public int Page { get; set; }
    public int Total { get; set; }
    public string? SortBy { get; set; }
    public bool Descending { get; set; }
    public string? Filter { get; set; }
}