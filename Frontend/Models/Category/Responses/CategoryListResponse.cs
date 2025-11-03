namespace Frontend.Models.Category.Responses;
using Frontend.Models.Common;

/// <summary>
/// Response cho danh sách categories có phân trang (để bọc trong ApiResponse)
/// </summary>
public class CategoryListResponse
{
    public List<CategoryDto> Categories { get; set; } = new();
    public PaginationInfo Pagination { get; set; } = new();
}