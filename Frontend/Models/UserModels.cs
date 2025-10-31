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
    // Single user
    public UserDto? User { get; set; }
    
    // List users
    public List<UserDto>? Users { get; set; }
    
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