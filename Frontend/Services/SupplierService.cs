using System.Net.Http.Json;
using Frontend.Models.Common;
using Frontend.Models.Supplier;
using Frontend.Models.Supplier.Requests;
using Frontend.Models.Supplier.Responses;

namespace Frontend.Services;

public class SupplierService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/suppliers";

    public SupplierService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Lấy tất cả suppliers (kể cả inactive) - cho admin
    public async Task<ApiResponse<SupplierListData>?> GetSuppliersAsync(
        int page = 1, 
        int pageSize = 10, 
        string? q = null)
    {
        try
        {
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={Uri.EscapeDataString(q)}";

            return await _httpClient.GetFromJsonAsync<ApiResponse<SupplierListData>>(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting suppliers: {ex.Message}");
            return new ApiResponse<SupplierListData>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Lấy chỉ suppliers đang hoạt động (cho dropdown khi thêm/sửa product)
    public async Task<ApiResponse<SupplierListData>?> GetActiveSuppliersAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<SupplierListData>>($"{BaseUrl}/active");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting active suppliers: {ex.Message}");
            return new ApiResponse<SupplierListData>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    public async Task<ApiResponse<SupplierListData>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await GetSuppliersAsync(page, pageSize) 
            ?? new ApiResponse<SupplierListData> { StatusCode = 500, Message = "Failed to fetch suppliers" };
    }

    public async Task<ApiResponse<SupplierDetailData>> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<SupplierDetailData>>($"{BaseUrl}/{id}")
            ?? new ApiResponse<SupplierDetailData> { StatusCode = 500, Message = "Failed to fetch supplier" };
    }

    public async Task<ApiResponse<SupplierDetailData>> CreateAsync(CreateSupplierRequest request)
    {
        var formData = new MultipartFormDataContent
        {
            { new StringContent(request.Name), "name" },
            { new StringContent(request.Phone ?? ""), "phone" },
            { new StringContent(request.Email ?? ""), "email" },
            { new StringContent(request.Address ?? ""), "address" }
        };

        var response = await _httpClient.PostAsync(BaseUrl, formData);
        return await response.Content.ReadFromJsonAsync<ApiResponse<SupplierDetailData>>()
            ?? new ApiResponse<SupplierDetailData> { StatusCode = 500, Message = "Failed to create supplier" };
    }

    public async Task<ApiResponse<SupplierDetailData>> UpdateAsync(string id, UpdateSupplierRequest request)
    {
        var formData = new MultipartFormDataContent
        {
            { new StringContent(request.Name), "name" },
            { new StringContent(request.Phone ?? ""), "phone" },
            { new StringContent(request.Email ?? ""), "email" },
            { new StringContent(request.Address ?? ""), "address" }
        };

        var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", formData);
        return await response.Content.ReadFromJsonAsync<ApiResponse<SupplierDetailData>>()
            ?? new ApiResponse<SupplierDetailData> { StatusCode = 500, Message = "Failed to update supplier" };
    }

    public async Task<ApiResponse<object>> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>()
            ?? new ApiResponse<object> { StatusCode = 500, Message = "Failed to delete supplier" };
    }
}