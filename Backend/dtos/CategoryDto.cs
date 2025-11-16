namespace Backend.Dtos;

public class CategoryDto
{
    public string Id { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "Active";
    public int ProductCount { get; set; } = 0;
}