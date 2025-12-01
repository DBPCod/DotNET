using Frontend.Models.Common;
using Frontend.Models.Product;

namespace Frontend.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public event Action? OnChange;

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
    }

    public void Remove(string productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            NotifyStateChanged();
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
    }

    public void Clear()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    public decimal GetTotal() => _items.Sum(x => x.Total);

    public int GetTotalQuantity() => _items.Sum(x => x.Quantity);

    private void NotifyStateChanged() => OnChange?.Invoke();
}


