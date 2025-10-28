namespace Backend.Dtos.Requests.User;

public class GetUsersRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Q { get; set; } // Search query
    public string? Role { get; set; } // Filter by role
    public string? Status { get; set; } // Filter by status
}