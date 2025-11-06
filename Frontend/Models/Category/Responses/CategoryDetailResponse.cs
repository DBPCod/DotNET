namespace Frontend.Models.Category.Responses;
using Frontend.Models.Category;

/// <summary>
/// Response cho chi tiết 1 category
/// </summary>
public class CategoryDetailResponse
{
    public CategoryDto Category { get; set; } = new();
}
