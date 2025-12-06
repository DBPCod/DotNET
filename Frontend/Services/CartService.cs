using Frontend.Models.Common;
using Frontend.Models.Product;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Frontend.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();
    private readonly IJSRuntime _jsRuntime;
    private const string CartStorageKey = "cart_items";
    private bool _isInitialized = false;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public event Action? OnChange;

    public CartService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public void AddToCart(ProductDto product, int quantity = 1)
    {
        if (quantity <= 0) quantity = 1;

        var existing = _items.FirstOrDefault(x => x.ProductId == product.Id);
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

    public void UpdateQuantity(string productId, int quantity)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return;

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

    // Lưu giỏ hàng vào localStorage
    private async Task SaveToLocalStorageAsync()
    {
        try
        {
            var cartJson = JsonSerializer.Serialize(_items);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CartStorageKey, cartJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving cart to localStorage: {ex.Message}");
        }
    }

    // Đọc giỏ hàng từ localStorage
    public async Task LoadFromLocalStorageAsync()
    {
        if (_isInitialized) return; // Chỉ load một lần

        try
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CartStorageKey);
            if (!string.IsNullOrEmpty(cartJson))
            {
                var items = JsonSerializer.Deserialize<List<CartItem>>(cartJson);
                if (items != null && items.Any())
                {
                    _items.Clear();
                    _items.AddRange(items);
                    NotifyStateChanged();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading cart from localStorage: {ex.Message}");
        }
        finally
        {
            _isInitialized = true;
        }
    }
}


