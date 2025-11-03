using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Category.Requests;

/// <summary>
/// Request để cập nhật thông tin category
/// </summary>
public class UpdateCategoryRequest
{
    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    public string CategoryName { get; set; } = "";

    public string Description { get; set; } = "";

    public bool IsActive { get; set; } = true;
}
