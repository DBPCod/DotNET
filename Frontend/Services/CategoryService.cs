using System.Net.Http.Json;
using Frontend.Models.Category;
using Frontend.Models.Category.Requests;
using Frontend.Models.Category.Responses;
using Frontend.Models.Common;

namespace Frontend.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/categories";

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Danh sách categories với phân trang và tìm kiếm
    public async Task<ApiResponse<CategoryListResponse>?> GetCategoriesAsync(
        int page = 1,
        int pageSize = 10,
        string? q = null)
    {
        try
        {
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(q))
                query += $"&q={Uri.EscapeDataString(q)}";

            return await _httpClient.GetFromJsonAsync<ApiResponse<CategoryListResponse>>(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting categories: {ex.Message}");
            return new ApiResponse<CategoryListResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Chi tiết category theo ID
    public async Task<ApiResponse<CategoryDetailResponse>?> GetCategoryByIdAsync(string id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<CategoryDetailResponse>>($"{BaseUrl}/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting category: {ex.Message}");
            return new ApiResponse<CategoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // POST - Tạo mới category
    public async Task<ApiResponse<CategoryDetailResponse>?> CreateCategoryAsync(CreateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating category: {ex.Message}");
            return new ApiResponse<CategoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // PUT - Cập nhật category
    public async Task<ApiResponse<CategoryDetailResponse>?> UpdateCategoryAsync(string id, UpdateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating category: {ex.Message}");
            return new ApiResponse<CategoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // DELETE - Xoá category (soft delete)
    public async Task<ApiResponse<CategoryDetailResponse>?> DeleteCategoryAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResponse<CategoryDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting category: {ex.Message}");
            return new ApiResponse<CategoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }
}