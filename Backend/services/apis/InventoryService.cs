using Backend.Dtos.Requests;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services.Apis;

public class InventoryService(InventoryRepository inventoryRepository)
{
    private readonly InventoryRepository _inventoryRepository = inventoryRepository;

    public async Task<Inventory> CreateAsync(CreateInventoryRequest request)
    {
        var inventory = new Inventory
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            UpdatedAt = DateTime.Now // Gán UpdatedAt
        };

        return await _inventoryRepository.AddAsync(inventory);
    }

    public async Task<bool> UpdateInventoryAsync(UpdateInventoryRequest request)
    {
        var inventory = await _inventoryRepository.GetByProductIdAsync(request.ProductId);

        if (inventory == null)
            throw new Exception("Inventory item not found for the given product ID.");

        // Cộng dồn số lượng
        inventory.Quantity += request.Quantity;
        inventory.UpdatedAt = DateTime.Now;

        await _inventoryRepository.UpdateAsync(inventory);
        return true;
    }
}
