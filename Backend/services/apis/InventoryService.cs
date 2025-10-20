namespace Backend.Services.Apis;

public class InventoryService(InventoryRepository inventoryRepository)
{
    private readonly InventoryRepository _inventoryRepository = inventoryRepository;

}
