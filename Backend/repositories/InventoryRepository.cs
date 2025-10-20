namespace Backend.Repositories;

public class InventoryRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

}
