using System.Net.Http.Json;
using Frontend.Models.Promotions;
using Frontend.Models.Promotions.Requests;
using Frontend.Models.Promotions.Responses;
using Frontend.Models.Common;

namespace Frontend.Services;

public class PromotionService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "/api/promotions";
    private static readonly System.Text.Json.JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };

    public PromotionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(List<PromotionDto> promotions, PaginationInfo? pagination)> GetPromotionsAsync(
        int page = 1, 
        int pageSize = 10, 
        string? searchTerm = null,
        PromotionStatus? status = null,
        DiscountType? discountType = null,
        PromotionType? promotionType = null,
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
            
            if (status.HasValue)
                queryParams.Add($"status={(status.Value == PromotionStatus.Active ? "active" : "inactive")}");
            
            if (discountType.HasValue)
            {
                var dt = discountType.Value switch
                {
                    DiscountType.Percent => "percent",
                    DiscountType.Fixed => "fixed",
                    DiscountType.FreeShipping => "free_shipping",
                    _ => null
                };
                if (dt != null) queryParams.Add($"discountType={dt}");
            }
            
            if (promotionType.HasValue)
            {
                var pt = promotionType.Value switch
                {
                    PromotionType.Promotion => "promotion",
                    PromotionType.DiscountCode => "discount_code",
                    _ => null
                };
                if (pt != null) queryParams.Add($"promotionType={pt}");
            }
            
            if (fromDate.HasValue)
                queryParams.Add($"from={fromDate.Value:yyyy-MM-dd}");
            
            if (toDate.HasValue)
                queryParams.Add($"to={toDate.Value:yyyy-MM-dd}");

            var url = $"{BaseUrl}?{string.Join("&", queryParams)}";
            
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PromotionListData>>(url, JsonOptions);
            
            if (response != null && response.StatusCode == 200 && response.Data != null)
            {
                return (response.Data.Promotions, response.Data.Pagination);
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
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PromotionDetailData>>($"{BaseUrl}/{id}", JsonOptions);
            
            if (response != null && response.StatusCode == 200 && response.Data != null)
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
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request, JsonOptions);
            
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
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request, JsonOptions);
            
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

    public string GetDiscountTypeDisplay(DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.Percent => "Giảm giá %",
            DiscountType.Fixed => "Giảm giá cố định",
            DiscountType.FreeShipping => "Miễn phí vận chuyển",
            _ => discountType.ToString()
        };
    }

    public string GetStatusDisplay(PromotionStatus status)
    {
        var today = DateTime.Today;
        
        return status switch
        {
            PromotionStatus.Active => "Đang hoạt động",
            PromotionStatus.Inactive => "Tạm dừng",
            _ => status.ToString()
        };
    }

    public string GetStatusBadgeClass(PromotionStatus status)
    {
        return status switch
        {
            PromotionStatus.Active => "bg-success",
            PromotionStatus.Inactive => "bg-secondary",
            _ => "bg-secondary"
        };
    }
}

