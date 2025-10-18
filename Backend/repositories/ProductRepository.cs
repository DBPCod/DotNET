namespace Backend.Repositories;

public class ProductRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

}
