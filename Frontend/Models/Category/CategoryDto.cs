namespace Frontend.Models.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = "";
    public string Description { get; set; } = "";
    // Trạng thái từ backend (Active/Deleted)
    public string Status { get; set; } = "";
    // Các field dưới đây có thể không có từ backend; dùng tạm cho UI nếu cần
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}