using System.Net.Http.Json;
using Frontend.Models.User;
using Frontend.Models.User.Requests;
using Frontend.Models.User.Responses;
using Frontend.Models.Common;

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
    public async Task<ApiResponse<UserListResponse>?> GetUsersAsync(
        int page = 1, 
        int pageSize = 10, 
        string? q = null, 
        string? role = null, 
        string? status = null)
    {
        try
        {
            Console.WriteLine("Fetching users from backend...");
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={q}";
            
            if (!string.IsNullOrEmpty(role))
                query += $"&role={role}";
            
            if (!string.IsNullOrEmpty(status))
                query += $"&status={status}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<UserListResponse>>(query);
            Console.WriteLine("Fetched users successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting users: {ex.Message}");
            Console.WriteLine($"Error getting users: {ex}");
            return new ApiResponse<UserListResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy user theo ID
    public async Task<ApiResponse<UserDetailResponse>?> GetUserByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<UserDetailResponse>>($"{BaseUrl}/{id}");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user: {ex.Message}");
            return new ApiResponse<UserDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // POST - Tạo user mới (Gửi JSON)
    public async Task<ApiResponse<UserDetailResponse>?> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return new ApiResponse<UserDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // PUT - Cập nhật user (Gửi JSON)
    public async Task<ApiResponse<UserDetailResponse>?> UpdateUserAsync(string id, UpdateUserRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return new ApiResponse<UserDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // Mở khóa user (set status = ACTIVE)
    public async Task<ApiResponse<UserDetailResponse>?> UnlockUserAsync(string id)
    {
        try
        {
            var updateRequest = new UpdateUserRequest
            {
                Status = "ACTIVE"
            };
            
            var response = await _httpClient.PutAsJsonAsync($"/api/v1/users/{id}", updateRequest);
            return await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error unlocking user: {ex.Message}");
            return null;
        }
    }

    // DELETE - Khoá user (soft delete)
    public async Task<ApiResponse<UserDetailResponse>?> DeleteUserAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
            return new ApiResponse<UserDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }
}