using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class UpdateCategoryRequest
{
    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string CategoryName { get; set; } = "";
}
