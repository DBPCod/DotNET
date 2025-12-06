using System.Net.Http.Json;
using Frontend.Models.Common;
using Frontend.Models;
using Frontend.Models.OrderItem.Responses;
using Frontend.Models.OrderItem; // Assuming CreateOrderItemsRequest and OrderItemInput are in this namespace

namespace Frontend.Services;

public class OrderItemService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/order-items";

    public OrderItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Lấy danh sách order items với phân trang (không filter)
    public async Task<ApiResponse<OrderItemListResponse>?> GetOrderItemsAsync(
        int page = 1, 
        int pageSize = 10)
    {
        try
        {
            Console.WriteLine("Fetching order items from backend...");
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<OrderItemListResponse>>(query);
            Console.WriteLine("Fetched order items successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting order items: {ex.Message}");
            Console.WriteLine($"Error getting order items: {ex}");
            return new ApiResponse<OrderItemListResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Order items theo orderId với phân trang
    public async Task<ApiResponse<OrderItemListResponse>?> GetOrderItemsByOrderIdAsync(
        Guid orderId, 
        int page = 1, 
        int pageSize = 10)
    {
        try
        {
            Console.WriteLine($"Fetching order items for order {orderId} from backend...");
            var query = $"{BaseUrl}/{orderId}?page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<OrderItemListResponse>>(query);
            Console.WriteLine("Fetched order items successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting order items for order {orderId}: {ex.Message}");
            Console.WriteLine($"Error getting order items: {ex}");
            return new ApiResponse<OrderItemListResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // POST - Tạo order items mới
    public async Task<ApiResponse<CreateOrderItemsResponse>?> CreateOrderItemsAsync(CreateOrderItemsRequest request)
    {
        try
        {
            Console.WriteLine("Creating new order items...");
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return new ApiResponse<CreateOrderItemsResponse>
                {
                    Message = $"HTTP {response.StatusCode}: {errorContent}",
                    StatusCode = (int)response.StatusCode
                };
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreateOrderItemsResponse>>();
            Console.WriteLine("Order items created successfully.");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating order items: {ex.Message}");
            return new ApiResponse<CreateOrderItemsResponse>
            {
                Message = ex.Message,
                StatusCode = 500
            };
        }
    }
}