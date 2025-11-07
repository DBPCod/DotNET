using System.Net.Http.Json;
using Frontend.Models.Customer;
using Frontend.Models.Customer.Requests;
using Frontend.Models.Customer.Responses;
using Frontend.Models.Common;

namespace Frontend.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/customers";

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Danh sách customers với phân trang và tìm kiếm
    public async Task<ApiResponse<CustomerListResponse>?> GetCustomersAsync(
        int page = 1,
        int pageSize = 10,
        string? q = null,
        string? status = null)
    {
        try
        {
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={Uri.EscapeDataString(q)}";
            
            if (!string.IsNullOrEmpty(status))
                query += $"&status={Uri.EscapeDataString(status)}";

            return await _httpClient.GetFromJsonAsync<ApiResponse<CustomerListResponse>>(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting customers: {ex.Message}");
            return new ApiResponse<CustomerListResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Chi tiết customer theo ID
    public async Task<ApiResponse<CustomerDetailResponse>?> GetCustomerByIdAsync(string id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<CustomerDetailResponse>>($"{BaseUrl}/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting customer: {ex.Message}");
            return new ApiResponse<CustomerDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // POST - Tạo customer mới
    public async Task<ApiResponse<CustomerDetailResponse>?> CreateCustomerAsync(CreateCustomerRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating customer: {ex.Message}");
            return new ApiResponse<CustomerDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // PUT - Cập nhật customer
    public async Task<ApiResponse<CustomerDetailResponse>?> UpdateCustomerAsync(string id, UpdateCustomerRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDetailResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating customer: {ex.Message}");
            return new ApiResponse<CustomerDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // DELETE - Xóa customer
    public async Task<ApiResponse<object>?> DeleteCustomerAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting customer: {ex.Message}");
            return new ApiResponse<object>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }
}