using System.Net.Http.Json;
using Frontend.Models.Common;
using Frontend.Models.Order.Responses;
using Frontend.Models.Order; // Assuming CreateOrderRequest is in this namespace

namespace Frontend.Services;

public class OrderService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/orders";

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Lấy danh sách orders với phân trang và filter
    public async Task<ApiResponse<OrderListResponse>?> GetOrdersAsync(
        int page = 1, 
        int pageSize = 10, 
        string? q = null,  // Tìm kiếm theo mã đơn hàng
        string? status = null,  // Lọc theo trạng thái (e.g., "PENDING", "PROCESSING", "DELIVERED", "CANCELLED")
        DateTime? fromDate = null,  // Lọc từ ngày
        DateTime? toDate = null,   // Lọc đến ngày
        Guid? customerId = null)   // Lọc theo customer ID
    {
        try
        {
            Console.WriteLine("Fetching orders from backend...");
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(q))
                query += $"&q={Uri.EscapeDataString(q)}";
            
            if (!string.IsNullOrEmpty(status))
                query += $"&status={Uri.EscapeDataString(status)}";
            
            if (fromDate.HasValue)
                query += $"&fromDate={fromDate.Value:yyyy-MM-dd}";
            
            if (toDate.HasValue)
                query += $"&toDate={toDate.Value:yyyy-MM-dd}";
            
            if (customerId.HasValue)
                query += $"&customerId={customerId.Value}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<OrderListResponse>>(query);
            Console.WriteLine("Fetched orders successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting orders: {ex.Message}");
            Console.WriteLine($"Error getting orders: {ex}");
            return new ApiResponse<OrderListResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // POST - Tạo order mới (Gửi JSON)
    public async Task<ApiResponse<OrderDetailResponse>?> CreateOrderAsync(CreateOrderRequest request)
    {
        try
        {
            Console.WriteLine("Creating new order...");
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return new ApiResponse<OrderDetailResponse>
                {
                    Message = $"HTTP {response.StatusCode}: {errorContent}",
                    StatusCode = (int)response.StatusCode
                };
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderDetailResponse>>();
            Console.WriteLine("Order created successfully.");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating order: {ex.Message}");
            return new ApiResponse<OrderDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy order theo ID
    public async Task<ApiResponse<OrderDetailResponse>?> GetOrderByIdAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<OrderDetailResponse>>($"{BaseUrl}/{id}");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting order: {ex.Message}");
            return new ApiResponse<OrderDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // PATCH - Cập nhật trạng thái order
    public async Task<ApiResponse<UpdateOrderStatusDto>?> UpdateOrderStatusAsync(Guid id, UpdateStatusOrderRequest request)
    {
        try
        {
            var response = await _httpClient.PatchAsJsonAsync($"{BaseUrl}/{id}/status", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<UpdateOrderStatusDto>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating order status: {ex.Message}");
            return new ApiResponse<UpdateOrderStatusDto> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // DELETE - Xóa order (soft delete hoặc hard delete tùy backend)
    // public async Task<ApiResponse<bool>?> DeleteOrderAsync(Guid id)
    // {
    //     try
    //     {
    //         var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
    //         var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
    //         return result;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error deleting order: {ex.Message}");
    //         return new ApiResponse<bool> 
    //         { 
    //             Message = ex.Message, 
    //             StatusCode = 500 
    //         };
    //     }
    // }

    // POST - Áp dụng promo cho order (nếu cần)
    // public async Task<ApiResponse<ApplyPromoResultDto>?> ApplyPromoAsync(Guid orderId, string promoCode)
    // {
    //     try
    //     {
    //         var request = new ApplyPromoRequest { Code = promoCode };
    //         var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{orderId}/apply-promo", request);
    //         var result = await response.Content.ReadFromJsonAsync<ApiResponse<ApplyPromoResultDto>>();
    //         return result;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error applying promo: {ex.Message}");
    //         return new ApiResponse<ApplyPromoResultDto> 
    //         { 
    //             Message = ex.Message, 
    //             StatusCode = 500 
    //         };
    //     }
    // }
}