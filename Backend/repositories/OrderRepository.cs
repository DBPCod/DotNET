namespace Backend.Repositories;

public class OrderRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

}
