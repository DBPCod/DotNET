namespace Backend.Repositories;

public class CustomerRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

}
