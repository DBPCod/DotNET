using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/users";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Lấy danh sách users với phân trang và filter
    public async Task<ApiResponse?> GetUsersAsync(
        int page = 1, 
        int pageSize = 10, 
        string? q = null, 
        string? role = null, 
        string? status = null)
    {
        try
        {
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={q}";
            
            if (!string.IsNullOrEmpty(role))
                query += $"&role={role}";
            
            if (!string.IsNullOrEmpty(status))
                query += $"&status={status}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse>(query);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting users: {ex.Message}");
            return new ApiResponse 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy user theo ID
    public async Task<ApiResponse?> GetUserByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{BaseUrl}/{id}");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user: {ex.Message}");
            return new ApiResponse 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // POST - Tạo user mới (Gửi JSON)
    public async Task<ApiResponse?> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return new ApiResponse 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // PUT - Cập nhật user (Gửi JSON)
    public async Task<ApiResponse?> UpdateUserAsync(string id, UpdateUserRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return new ApiResponse 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // DELETE - Xóa user (soft delete)
    public async Task<ApiResponse?> DeleteUserAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
            return new ApiResponse 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }
}