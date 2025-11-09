using System.Net.Http.Json;
using Backend.Dtos.Requests.Payment;
using Frontend.Models;
using Frontend.Models.Common;
using static Frontend.Pages.Admin.Orders;

namespace Frontend.Services;

public class PaymentService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/payments";

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // POST - Tạo payment mới (Gửi JSON)
    public async Task<ApiResponse<PaymentDto>?> CreatePaymentAsync(CreatePaymentRequest request)
    {
        try
        {
            Console.WriteLine("Creating new payment...");
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return new ApiResponse<PaymentDto>
                {
                    Message = $"HTTP {response.StatusCode}: {errorContent}",
                    StatusCode = (int)response.StatusCode
                };
            }

            // Backend trả { message, payment }, parse thủ công vì không wrap trong ApiResponse
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw response content: {content}");
            var jsonDoc = System.Text.Json.JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            var apiResponse = new ApiResponse<PaymentDto>
            {
                StatusCode = (int)response.StatusCode,
                Message = root.TryGetProperty("message", out var msgProp) ? msgProp.GetString() ?? "Success" : "Payment created"
            };

            if (root.TryGetProperty("payment", out var paymentProp) && paymentProp.ValueKind != System.Text.Json.JsonValueKind.Null)
            {
                apiResponse.Data = System.Text.Json.JsonSerializer.Deserialize<PaymentDto>(paymentProp.GetRawText(), new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            Console.WriteLine("Payment created successfully.");
            return apiResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating payment: {ex.Message}");
            Console.WriteLine($"Error creating payment: {ex}");
            return new ApiResponse<PaymentDto> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy tất cả payments
    public async Task<ApiResponse<List<PaymentDto>>?> GetAllPaymentsAsync()
    {
        try
        {
            Console.WriteLine("Fetching all payments from backend...");
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PaymentDto>>>(BaseUrl);
            Console.WriteLine("Fetched all payments successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all payments: {ex.Message}");
            Console.WriteLine($"Error getting all payments: {ex}");
            return new ApiResponse<List<PaymentDto>> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy payments theo OrderId
    public async Task<ApiResponse<PaymentListData>?> GetPaymentsByOrderIdAsync(Guid orderId)
    {
        try
        {
            Console.WriteLine($"Fetching payments for order {orderId} from backend...");
            var query = $"{BaseUrl}/order/{orderId}";

            // Deserialize đúng kiểu
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PaymentListData>>(query);

            Console.WriteLine("Fetched payments by order successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting payments by order: {ex.Message}");
            Console.WriteLine($"Error getting payments by order: {ex}");
            return new ApiResponse<PaymentListData>
            {
                Message = ex.Message,
                StatusCode = 500,
                Data = new PaymentListData()
            };
        }
    }

}