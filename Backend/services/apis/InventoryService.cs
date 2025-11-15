using Backend.Dtos;
using Backend.Dtos.Requests;
using Backend.Models;
using Backend.Repositories;
using Backend.Utils.Mappers;

namespace Backend.Services.Apis;

public class InventoryService(InventoryRepository inventoryRepository)
{
    private readonly InventoryRepository _inventoryRepository = inventoryRepository;

    public async Task<Inventory> CreateAsync(CreateInventoryRequest request)
    {
        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new Exception("Invalid Product ID format");
        }

        // Check if inventory already exists for this product
        var existingInventory = await _inventoryRepository.GetByProductIdAsync(productId);
        if (existingInventory != null)
        {
            throw new Exception("Inventory already exists for this product. Use update instead.");
        }

        var inventory = new Inventory
        {
            ProductId = productId,
            Quantity = request.Quantity,
            CostPrice = request.CostPrice,
            UpdatedAt = DateTime.Now
        };

        return await _inventoryRepository.AddAsync(inventory);
    }

    public async Task<InventoryDto?> GetByIdAsync(Guid id)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        return inventory != null ? MapToDto(inventory) : null;
    }

    public async Task<InventoryDto?> GetByProductIdAsync(Guid productId)
    {
        var inventory = await _inventoryRepository.GetByProductIdAsync(productId);
        return inventory != null ? MapToDto(inventory) : null;
    }

    public async Task<(List<InventoryDto> Items, int TotalCount)> GetAllAsync(
        int page = 1,
        int pageSize = 10,
        string? searchQuery = null,
        string? status = null,
        Guid? categoryId = null,
        int? minQuantity = null)
    {
        var (inventories, totalCount) = await _inventoryRepository.GetAllAsync(
            page, pageSize, searchQuery, status, categoryId, minQuantity);

        var dtos = inventories.Select(MapToDto).ToList();
        return (dtos, totalCount);
    }

    public async Task<InventoryDto> UpdateAsync(Guid id, UpdateInventoryRequest request)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        if (inventory == null)
            throw new Exception("Inventory not found.");

        inventory.Quantity = request.Quantity;
        inventory.CostPrice = request.CostPrice;
        inventory.UpdatedAt = DateTime.Now;

        await _inventoryRepository.UpdateAsync(inventory);
        return MapToDto(inventory);
    }

    public async Task<bool> UpdateInventoryAsync(UpdateInventoryRequest request)
    {
        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new Exception("Invalid Product ID format");
        }

        var inventory = await _inventoryRepository.GetByProductIdAsync(productId);

        if (inventory == null)
            throw new Exception("Inventory item not found for the given product ID.");

        // Set absolute quantity instead of adding
        inventory.Quantity = request.Quantity;
        inventory.CostPrice = request.CostPrice;
        inventory.UpdatedAt = DateTime.Now;

        await _inventoryRepository.UpdateAsync(inventory);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(id);
        if (inventory == null)
            return false;

        await _inventoryRepository.DeleteAsync(inventory);
        return true;
    }

    public async Task<object> GetStatsAsync()
    {
        var (inStock, outOfStock, totalValue) = await _inventoryRepository.GetStatsAsync();
        
        return new
        {
            TotalItems = inStock + outOfStock,
            InStockItems = inStock,
            OutOfStockItems = outOfStock,
            TotalInventoryValue = totalValue
        };
    }

    private InventoryDto MapToDto(Inventory inventory)
    {
        return new InventoryDto
        {
            Id = inventory.Id.ToString(),
            ProductId = inventory.ProductId.ToString(),
            Quantity = inventory.Quantity,
            CostPrice = inventory.CostPrice,
            UpdatedAt = inventory.UpdatedAt,
            // Navigation properties
            ProductName = inventory.Product?.ProductName,
            ProductBarcode = inventory.Product?.Barcode,
            ProductPrice = inventory.Product?.Price,
            ProductUnit = inventory.Product?.Unit,
            CategoryName = inventory.Product?.Category?.CategoryName,
            SupplierName = inventory.Product?.Supplier?.Name,
            ProductImagePath = inventory.Product?.ImagePath,
            ProductStatus = inventory.Product?.Status
        };
    }
}
