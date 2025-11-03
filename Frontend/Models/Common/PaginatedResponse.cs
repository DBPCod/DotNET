namespace Frontend.Models.Common;

/// <summary>
/// Response wrapper cho dữ liệu có phân trang
/// </summary>
public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}