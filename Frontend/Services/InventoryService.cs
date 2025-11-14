using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Frontend.Models.Common;
using Frontend.Models.Inventory;
using Frontend.Models.Inventory.Requests;
using Frontend.Models.Inventory.Responses;

namespace Frontend.Services;

public class InventoryService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/inventories";

    public InventoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Danh sách inventories với phân trang và tìm kiếm
    public async Task<ApiResponse<InventoryListResponse>?> GetInventoriesAsync(
        int page = 1,
        int pageSize = 10,
        string? q = null,
        string? status = null,
        string? categoryId = null,
        int? minQuantity = null)
    {
        try
        {
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={Uri.EscapeDataString(q)}";
            
            if (!string.IsNullOrEmpty(status))
                query += $"&status={Uri.EscapeDataString(status)}";
                
            if (!string.IsNullOrEmpty(categoryId))
                query += $"&categoryId={Uri.EscapeDataString(categoryId)}";
                
            if (minQuantity.HasValue)
                query += $"&minQuantity={minQuantity.Value}";

            return await _httpClient.GetFromJsonAsync<ApiResponse<InventoryListResponse>>(query);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting inventories: {ex.Message}");
            return new ApiResponse<InventoryListResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Chi tiết inventory theo ID
    public async Task<ApiResponse<InventoryDetailResponse>?> GetInventoryByIdAsync(string id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<InventoryDetailResponse>>($"{BaseUrl}/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting inventory by id: {ex.Message}");
            return new ApiResponse<InventoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Chi tiết inventory theo ProductId
    public async Task<ApiResponse<InventoryDetailResponse>?> GetInventoryByProductIdAsync(string productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<InventoryDetailResponse>>($"{BaseUrl}/product/{productId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting inventory by product id: {ex.Message}");
            return new ApiResponse<InventoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // POST - Tạo inventory mới
    public async Task<ApiResponse<InventoryDetailResponse>?> CreateInventoryAsync(CreateInventoryRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(BaseUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<ApiResponse<InventoryDetailResponse>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating inventory: {ex.Message}");
            return new ApiResponse<InventoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // PUT - Cập nhật inventory
    public async Task<ApiResponse<InventoryDetailResponse>?> UpdateInventoryAsync(string id, UpdateInventoryRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<ApiResponse<InventoryDetailResponse>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating inventory: {ex.Message}");
            return new ApiResponse<InventoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // PUT - Điều chỉnh số lượng tồn kho
    public async Task<ApiResponse<InventoryDetailResponse>?> AdjustInventoryAsync(AdjustInventoryRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{BaseUrl}/update", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<ApiResponse<InventoryDetailResponse>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adjusting inventory: {ex.Message}");
            return new ApiResponse<InventoryDetailResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // DELETE - Xóa inventory
    public async Task<ApiResponse<object>?> DeleteInventoryAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<ApiResponse<object>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting inventory: {ex.Message}");
            return new ApiResponse<object>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // GET - Thống kê tồn kho
    public async Task<ApiResponse<InventoryStatsResponse>?> GetInventoryStatsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<InventoryStatsResponse>>($"{BaseUrl}/stats");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting inventory stats: {ex.Message}");
            return new ApiResponse<InventoryStatsResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }

    // Utility methods
    public static string FormatPrice(decimal price)
    {
        return price.ToString("N0") + "₫";
    }
    
    public static string GetStatusDisplayName(string status)
    {
        return status?.ToUpper() switch
        {
            "IN_STOCK" => "Còn hàng",
            "LOW_STOCK" => "Sắp hết hàng", 
            "OUT_OF_STOCK" => "Hết hàng",
            _ => "Không xác định"
        };
    }
}