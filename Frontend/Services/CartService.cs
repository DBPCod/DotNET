using Frontend.Models.Common;
using Frontend.Models.Product;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Frontend.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthService _authService;
    private bool _isInitialized = false;
    private string? _currentUserId = null; // Track user ID để phát hiện user thay đổi

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public event Action? OnChange;

    public CartService(IJSRuntime jsRuntime, AuthService authService)
    {
        _jsRuntime = jsRuntime;
        _authService = authService;
    }

    // Helper để lấy storage key theo userId
    private string GetCartStorageKey()
    {
        var userId = _authService.CurrentUser?.Id;
        if (string.IsNullOrEmpty(userId))
        {
            // Nếu chưa đăng nhập, dùng key mặc định (guest cart)
            return "cart_items_guest";
        }
        return $"cart_items_{userId}";
    }

    public bool AddToCart(ProductDto product, int quantity = 1, int? maxQuantity = null)
    {
        if (quantity <= 0) quantity = 1;

        var existing = _items.FirstOrDefault(x => x.ProductId == product.Id);
        
        // Kiểm tra số lượng tồn kho nếu có
        if (maxQuantity.HasValue)
        {
            int currentQuantityInCart = existing?.Quantity ?? 0;
            int totalQuantity = currentQuantityInCart + quantity;
            
            if (totalQuantity > maxQuantity.Value)
            {
                // Không thêm được, vượt quá số lượng tồn
                return false;
            }
        }
        
        if (existing is null)
        {
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.Price,
                Quantity = quantity,
                ImagePath = product.ImagePath
            });
        }
        else
        {
            existing.Quantity += quantity;
        }

        NotifyStateChanged();
        _ = SaveToLocalStorageAsync(); // Fire-and-forget
        return true;
    }

    public void Remove(string productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            NotifyStateChanged();
            _ = SaveToLocalStorageAsync(); // Fire-and-forget
        }
    }

    public bool UpdateQuantity(string productId, int quantity, int? maxQuantity = null)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return false;

        // Kiểm tra số lượng tồn kho nếu có
        if (maxQuantity.HasValue && quantity > maxQuantity.Value)
        {
            return false; // Không cho phép cập nhật
        }

        if (quantity <= 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        NotifyStateChanged();
        _ = SaveToLocalStorageAsync(); // Fire-and-forget
        return true;
    }

    public void Clear()
    {
        _items.Clear();
        NotifyStateChanged();
        _ = SaveToLocalStorageAsync(); // Fire-and-forget
    }

    public decimal GetTotal() => _items.Sum(x => x.Total);

    public int GetTotalQuantity() => _items.Sum(x => x.Quantity);

    private void NotifyStateChanged() => OnChange?.Invoke();

    // Xóa giỏ hàng khi đăng xuất (chỉ xóa trong memory, giữ lại trong localStorage)
    public async Task ClearCartOnLogout()
    {
        Console.WriteLine($"[CartService] ClearCartOnLogout - CurrentUserId: {_currentUserId}, Items count: {_items.Count}");
        
        // Lưu giỏ hàng hiện tại vào localStorage trước khi xóa (đảm bảo dữ liệu được lưu)
        if (_items.Any())
        {
            Console.WriteLine($"[CartService] Saving cart to localStorage before logout");
            await SaveToLocalStorageAsync();
        }
        
        // Chỉ xóa giỏ hàng trong memory, KHÔNG xóa trong localStorage
        // Để user có thể xem lại giỏ hàng khi đăng nhập lại
        _items.Clear();
        _isInitialized = false;
        _currentUserId = null;
        NotifyStateChanged();
        
        Console.WriteLine($"[CartService] ClearCartOnLogout completed - Cart cleared from memory, kept in localStorage");
    }

    // Lưu giỏ hàng vào localStorage
    private async Task SaveToLocalStorageAsync()
    {
        try
        {
            var cartJson = JsonSerializer.Serialize(_items);
            var storageKey = GetCartStorageKey();
            Console.WriteLine($"[CartService] Saving cart to localStorage - Key: {storageKey}, Items: {_items.Count}");
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", storageKey, cartJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CartService] Error saving cart to localStorage: {ex.Message}");
        }
    }

    // Force reload giỏ hàng (bỏ qua flag _isInitialized)
    public async Task ForceReloadFromLocalStorageAsync()
    {
        _isInitialized = false;
        await LoadFromLocalStorageAsync();
    }

    // Đọc giỏ hàng từ localStorage
    public async Task LoadFromLocalStorageAsync()
    {
        var currentUserId = _authService.CurrentUser?.Id;
        
        Console.WriteLine($"[CartService] LoadFromLocalStorageAsync - CurrentUserId: {currentUserId}, PreviousUserId: {_currentUserId}, IsInitialized: {_isInitialized}");
        
        // Nếu user đã thay đổi (từ guest sang user hoặc từ user này sang user khác), 
        // xóa giỏ hàng cũ trong memory và reset flag
        if (_currentUserId != currentUserId)
        {
            Console.WriteLine($"[CartService] User changed, clearing cart in memory");
            _items.Clear();
            _isInitialized = false;
            NotifyStateChanged();
        }
        
        // Nếu đã initialized và user không đổi, không cần load lại
        if (_isInitialized && _currentUserId == currentUserId)
        {
            Console.WriteLine($"[CartService] Already initialized with same user, skipping load");
            return;
        }

        try
        {
            var storageKey = GetCartStorageKey();
            Console.WriteLine($"[CartService] Loading cart from storage key: {storageKey}");
            
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", storageKey);
            if (!string.IsNullOrEmpty(cartJson))
            {
                var items = JsonSerializer.Deserialize<List<CartItem>>(cartJson);
                if (items != null && items.Any())
                {
                    Console.WriteLine($"[CartService] Loaded {items.Count} items from localStorage");
                    _items.Clear();
                    _items.AddRange(items);
                    NotifyStateChanged();
                }
                else
                {
                    Console.WriteLine($"[CartService] No items found in cart JSON");
                }
            }
            else
            {
                Console.WriteLine($"[CartService] No cart data in localStorage for key: {storageKey}");
                // Nếu không có giỏ hàng trong localStorage, đảm bảo giỏ hàng trong memory cũng trống
                if (_items.Any())
                {
                    _items.Clear();
                    NotifyStateChanged();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CartService] Error loading cart from localStorage: {ex.Message}");
            Console.WriteLine($"[CartService] Stack trace: {ex.StackTrace}");
        }
        finally
        {
            _isInitialized = true;
            _currentUserId = currentUserId;
            Console.WriteLine($"[CartService] LoadFromLocalStorageAsync completed - CurrentUserId: {_currentUserId}, Items count: {_items.Count}");
        }
    }
}


