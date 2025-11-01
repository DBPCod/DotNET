using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class PromotionService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:4040/api/promotions";

    public PromotionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(List<PromotionDto> promotions, PaginationInfo? pagination)> GetPromotionsAsync(
        int page = 1, 
        int pageSize = 10, 
        string? searchTerm = null,
        string? status = null,
        string? discountType = null,
        string? promotionType = null,
        DateTime? fromDate = null,
        DateTime? toDate = null)
    {
        try
        {
            var queryParams = new List<string>
            {
                $"page={page}",
                $"pageSize={pageSize}"
            };

            if (!string.IsNullOrEmpty(searchTerm))
                queryParams.Add($"q={Uri.EscapeDataString(searchTerm)}");
            
            if (!string.IsNullOrEmpty(status))
                queryParams.Add($"status={Uri.EscapeDataString(status)}");
            
            if (!string.IsNullOrEmpty(discountType))
                queryParams.Add($"discountType={Uri.EscapeDataString(discountType)}");
            
            if (!string.IsNullOrEmpty(promotionType))
                queryParams.Add($"promotionType={Uri.EscapeDataString(promotionType)}");
            
            if (fromDate.HasValue)
                queryParams.Add($"from={fromDate.Value:yyyy-MM-dd}");
            
            if (toDate.HasValue)
                queryParams.Add($"to={toDate.Value:yyyy-MM-dd}");

            var url = $"{BaseUrl}?{string.Join("&", queryParams)}";
            
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
            
            if (response != null && response.StatusCode == 200)
            {
                return (response.Data.Promotions ?? new List<PromotionDto>(), response.Data.Pagination);
            }
            
            return (new List<PromotionDto>(), null);
        }
        catch (Exception)
        {
            return (new List<PromotionDto>(), null);
        }
    }

    public async Task<PromotionDto?> GetPromotionByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{BaseUrl}/{id}");
            
            if (response != null && response.StatusCode == 200)
            {
                return response.Data.Promotion;
            }
            
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> CreatePromotionAsync(CreatePromotionRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdatePromotionAsync(string id, UpdatePromotionRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeletePromotionAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string GetDiscountTypeDisplay(string discountType)
    {
        return discountType switch
        {
            "percent" => "Giảm giá %",
            "fixed" => "Giảm giá cố định",
            "free_shipping" => "Miễn phí vận chuyển",
            _ => discountType
        };
    }

    public string GetStatusDisplay(string status)
    {
        var today = DateTime.Today;
        
        // Note: Cần StartDate và EndDate để xác định "Sắp bắt đầu" và "Đã kết thúc"
        // Nhưng trong method này chỉ có status, nên chỉ hiển thị active/inactive
        return status switch
        {
            "active" => "Đang hoạt động",
            "inactive" => "Tạm dừng",
            _ => status
        };
    }

    public string GetStatusBadgeClass(string status)
    {
        return status switch
        {
            "active" => "bg-success",
            "inactive" => "bg-secondary",
            _ => "bg-secondary"
        };
    }
}

