using System.Net.Http.Json;
using Frontend.Models.Product;
using Frontend.Models.Product.Requests;
using Frontend.Models.Product.Responses;
using Frontend.Models.Common;

namespace Frontend.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/v1/products";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET - Lấy danh sách products với phân trang và filter
    public async Task<ApiResponse<ProductListResponse>?> GetProductsAsync(
        int page = 1, 
        int pageSize = 10, 
        string? searchQuery = null,
        string? categoryId = null,
        string? supplierId = null,
        bool? status = null)
    {
        try
        {
            Console.WriteLine("Fetching products from backend...");
            var query = $"{BaseUrl}?page={page}&pageSize={pageSize}";
            
            if (!string.IsNullOrEmpty(searchQuery))
                query += $"&q={Uri.EscapeDataString(searchQuery)}";
            
            if (!string.IsNullOrEmpty(categoryId))
                query += $"&categoryId={categoryId}";
            
            if (!string.IsNullOrEmpty(supplierId))
                query += $"&supplierId={supplierId}";
            
            if (status.HasValue)
                query += $"&status={status.Value}";

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ProductListResponse>>(query);
            Console.WriteLine("Fetched products successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting products: {ex.Message}");
            return new ApiResponse<ProductListResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // GET - Lấy product theo ID
    public async Task<ApiResponse<ProductDetailResponse>?> GetProductByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ProductDetailResponse>>($"{BaseUrl}/{id}");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting product: {ex.Message}");
            return new ApiResponse<ProductDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // POST - Tạo product mới (Gửi multipart/form-data với file)
    public async Task<ApiResponse<ProductDetailResponse>?> CreateProductAsync(CreateProductRequest request)
    {
        try
        {
            var formData = new MultipartFormDataContent();
            
            formData.Add(new StringContent(request.ProductName), "ProductName");
            formData.Add(new StringContent(request.Price.ToString("F2")), "Price");
            formData.Add(new StringContent(request.Unit), "Unit");
            formData.Add(new StringContent(request.Status.ToString().ToLower()), "Status");
            
            if (!string.IsNullOrEmpty(request.Barcode))
                formData.Add(new StringContent(request.Barcode), "Barcode");
            
            if (!string.IsNullOrEmpty(request.CategoryId))
                formData.Add(new StringContent(request.CategoryId), "CategoryId");
            
            if (!string.IsNullOrEmpty(request.SupplierId))
                formData.Add(new StringContent(request.SupplierId), "SupplierId");
            
            // Handle image upload
            if (request.Image != null)
            {
                var imageContent = new StreamContent(request.Image.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024));
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);
                formData.Add(imageContent, "Image", request.Image.Name);
            }

            var response = await _httpClient.PostAsync(BaseUrl, formData);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return new ApiResponse<ProductDetailResponse> 
                { 
                    Message = $"Server error: {response.StatusCode}", 
                    StatusCode = (int)response.StatusCode 
                };
            }
            
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating product: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return new ApiResponse<ProductDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // PUT - Cập nhật product (Gửi multipart/form-data với file)
    public async Task<ApiResponse<ProductDetailResponse>?> UpdateProductAsync(string id, UpdateProductRequest request)
    {
        try
        {
            var formData = new MultipartFormDataContent();
            
            formData.Add(new StringContent(request.ProductName), "ProductName");
            formData.Add(new StringContent(request.Price.ToString("F2")), "Price");
            formData.Add(new StringContent(request.Unit), "Unit");
            formData.Add(new StringContent(request.Status.ToString().ToLower()), "Status");
            
            if (!string.IsNullOrEmpty(request.Barcode))
                formData.Add(new StringContent(request.Barcode), "Barcode");
            
            if (!string.IsNullOrEmpty(request.CategoryId))
                formData.Add(new StringContent(request.CategoryId), "CategoryId");
            
            if (!string.IsNullOrEmpty(request.SupplierId))
                formData.Add(new StringContent(request.SupplierId), "SupplierId");
            
            // Handle image upload
            if (request.Image != null)
            {
                var imageContent = new StreamContent(request.Image.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024));
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);
                formData.Add(imageContent, "Image", request.Image.Name);
            }

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", formData);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return new ApiResponse<ProductDetailResponse> 
                { 
                    Message = $"Server error: {response.StatusCode}", 
                    StatusCode = (int)response.StatusCode 
                };
            }
            
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return new ApiResponse<ProductDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // DELETE - Xóa product (soft delete - chuyển sang ngừng bán)
    public async Task<ApiResponse<ProductDetailResponse>?> DeleteProductAsync(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDetailResponse>>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
            return new ApiResponse<ProductDetailResponse> 
            { 
                Message = ex.Message, 
                StatusCode = 500 
            };
        }
    }

    // Helper method để format giá
    public string FormatPrice(decimal price)
    {
        return $"{price:N0} VNĐ";
    }
}