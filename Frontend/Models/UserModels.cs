namespace Frontend.Models;

// Response từ API
public class ApiResponse
{
    public string Message { get; set; } = "";
    public int StatusCode { get; set; }
    public ResponseData Data { get; set; } = new();
}

public class ResponseData
{
    // User
    public UserDto? User { get; set; }
    public List<UserDto>? Users { get; set; }
    
    // Promotion
    public List<PromotionDto>? Promotions { get; set; }
    public PromotionDto? Promotion { get; set; }
    
    // Pagination
    public PaginationInfo? Pagination { get; set; }
}

// DTO User
public class UserDto
{
    public string Id { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}

// Pagination Info
public class PaginationInfo
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

// Request để tạo user
public class CreateUserRequest
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "STAFF";
}

// Request để update user
public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public string? Status { get; set; }
}