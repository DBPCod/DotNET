using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<User?> HandleGetUserByEmail(string email)
    {
        try
        {
            return await _context.User
                .FirstOrDefaultAsync(c => c.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User?> HandleGetUserByUsername(string username)
    {
        try
        {
            return await _context.User
                .FirstOrDefaultAsync(c => c.Username == username);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> HandleCreateUser(User user)
    {
        try
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> HandleUpdateUser(User user)
    {
        try
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // để get user theo id
    public async Task<User?> HandleGetUserById(Guid id)
      {
        try
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // cái này để get all
    public async Task<List<User>> HandleGetAllUsers()
    {
        try
        {
            return await _context.User
                .OrderBy(u => u.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // cái này để get user có phân trang
    public async Task<(List<User> users, int totalCount)> HandleGetUsersWithPagination(int page, int pageSize)
    {
        try
        {
            var query = _context.User.AsQueryable();
            var totalCount = await query.CountAsync();
            
            var users = await query
                .OrderBy(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> HandleDeleteUser(Guid id)
    {
        try
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
