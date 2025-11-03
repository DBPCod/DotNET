using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public class CreateCategoryRequest
{
    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string CategoryName { get; set; } = "";

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = "";
    
    public bool IsActive { get; set; } = true;  // Mặc định là true (status = 1)
}
