namespace Frontend.Models.Common;

/// <summary>
/// Generic API response wrapper cho tất cả API calls
/// </summary>
public class ApiResponse<T>
{
    public string Message { get; set; } = "";
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    
    public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
}

/// <summary>
/// API response không có data (chỉ message)
/// </summary>
public class ApiResponse : ApiResponse<object>
{
}