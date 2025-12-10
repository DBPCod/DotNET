using Backend.Dtos.Requests.User;
using Backend.Repositories;
using Backend.Contexts;

namespace Backend.Services.Apis;

public class UserService(UserRepository userRepository, CustomerRepository customerRepository, AppDbContext context)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly AppDbContext _context = context;

    public async Task<User?> HandleGetUserById(Guid id)
    {
        return await _userRepository.HandleGetUserById(id);
    }

    public async Task<(List<User> users, int totalCount)> HandleGetUsersWithPagination(
        int page, int pageSize, string? searchTerm = null, string? role = null, string? status = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        return await _userRepository.HandleGetUsersWithPagination(
            page, pageSize, searchTerm, role, status);
    }

    // Overload để giữ tương thích với code cũ
    public async Task<User?> HandleCreateUser(string username, string email, string password, string role, string fullName)
    {
        var existingUserByEmail = await _userRepository.HandleGetUserByEmail(email);
        if (existingUserByEmail != null)
            throw new ExceptionCustom(409, "Email này đã tồn tại");

        var existingUserByUsername = await _userRepository.HandleGetUserByUsername(username);
        if (existingUserByUsername != null)
            throw new ExceptionCustom(409, "Tên người dùng này đã tồn tại");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Username = username,
            FullName = fullName ?? "",
            Email = email,
            Password = hashedPassword,
            Role = string.IsNullOrEmpty(role) ? UserRole.USER : Enum.Parse<UserRole>(role),
            Status = UserStatus.ACTIVE // Tự động kích hoạt khi đăng ký (không cần OTP)
        };

        // Sử dụng transaction để đảm bảo tạo User và Customer cùng lúc
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tạo User
            var createdUser = await _userRepository.HandleCreateUser(newUser);

            // Tự động tạo Customer tương ứng (nếu chưa có)
            var existingCustomer = await _customerRepository.GetByEmailAsync(email);
            if (existingCustomer == null)
            {
                var lastCustomer = await _customerRepository.GetLastCustomerAsync();
                var nextNumber = 1;
                
                if (lastCustomer?.CustomerId != null && lastCustomer.CustomerId.StartsWith("CUS"))
                {
                    var numberPart = lastCustomer.CustomerId.Substring(3);
                    if (int.TryParse(numberPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                
                var customerId = $"CUS{nextNumber:D3}";
                
                var newCustomer = new Customer
                {
                    CustomerId = customerId,
                    Name = fullName ?? username,
                    Email = email,
                    Phone = null,
                    Address = null,
                    Status = "ACTIVE"
                };
                
                await _customerRepository.AddAsync(newCustomer);
            }

            await transaction.CommitAsync();
            return createdUser;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // Method mới nhận CreateUserRequest (FormData)
    public async Task<User?> HandleCreateUser(CreateUserRequest request)
    {
        // Check if email already exists
        var existingUserByEmail = await _userRepository.HandleGetUserByEmail(request.Email);
        if (existingUserByEmail != null)
            throw new ExceptionCustom(409, "Email này đã tồn tại");

        // Check if username already exists
        var existingUserByUsername = await _userRepository.HandleGetUserByUsername(request.Username);
        if (existingUserByUsername != null)
            throw new ExceptionCustom(409, "Tên người dùng này đã tồn tại");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new User
        {
            Username = request.Username.Trim(),
            Email = request.Email.Trim().ToLower(),
            Password = hashedPassword,
            FullName = request.FullName?.Trim() ?? "",
            Role = Enum.Parse<UserRole>(request.Role),
            Status = Enum.Parse<UserStatus>(request.Status)
        };

        // Sử dụng transaction để đảm bảo tạo User và Customer cùng lúc
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tạo User
            var createdUser = await _userRepository.HandleCreateUser(newUser);

            // Tự động tạo Customer tương ứng (nếu chưa có)
            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);
            if (existingCustomer == null)
            {
                var lastCustomer = await _customerRepository.GetLastCustomerAsync();
                var nextNumber = 1;
                
                if (lastCustomer?.CustomerId != null && lastCustomer.CustomerId.StartsWith("CUS"))
                {
                    var numberPart = lastCustomer.CustomerId.Substring(3);
                    if (int.TryParse(numberPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                
                var customerId = $"CUS{nextNumber:D3}";
                
                var newCustomer = new Customer
                {
                    CustomerId = customerId,
                    Name = request.FullName?.Trim() ?? request.Username.Trim(),
                    Email = request.Email.Trim().ToLower(),
                    Phone = null,
                    Address = null,
                    Status = "ACTIVE"
                };
                
                await _customerRepository.AddAsync(newCustomer);
            }

            await transaction.CommitAsync();
            return createdUser;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<User?> HandleUpdateUser(Guid id, UpdateUserRequest request)
    {
        var user = await _userRepository.HandleGetUserById(id);
        if (user == null)
            throw new ExceptionCustom(404, "User not found");

        // Check if email already exists (if being changed)
        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email)
        {
            var existingUserByEmail = await _userRepository.HandleGetUserByEmail(request.Email);
            if (existingUserByEmail != null)
                throw new ExceptionCustom(409, "Email này đã tồn tại");
            user.Email = request.Email.Trim().ToLower();
        }

        // Check if username already exists (if being changed)
        if (!string.IsNullOrEmpty(request.Username) && request.Username != user.Username)
        {
            var existingUserByUsername = await _userRepository.HandleGetUserByUsername(request.Username);
            if (existingUserByUsername != null)
                throw new ExceptionCustom(409, "Tên người dùng này đã tồn tại");
            user.Username = request.Username.Trim();
        }

        // Update password if provided
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        // Update full name if provided
        if (!string.IsNullOrEmpty(request.FullName))
        {
            user.FullName = request.FullName.Trim();
        }

        // Update role if provided
        if (!string.IsNullOrEmpty(request.Role))
        {
            user.Role = Enum.Parse<UserRole>(request.Role);
        }

        // Update status if provided
        if (!string.IsNullOrEmpty(request.Status))
        {
            user.Status = Enum.Parse<UserStatus>(request.Status);
        }

        return await _userRepository.HandleUpdateUser(user);
    }

    public async Task<bool> HandleSoftDeleteUser(Guid id)
    {
        var user = await _userRepository.HandleGetUserById(id);
        if (user == null)
            throw new ExceptionCustom(404, "Không tìm thấy người dùng này");

        return await _userRepository.HandleSoftDeleteUser(id);
    }

    public async Task<User?> HandleGetUserByEmail(string email)
    {
        return await _userRepository.HandleGetUserByEmail(email);
    }

    public async Task<User?> HandleGetUserByUsername(string username)
    {
        return await _userRepository.HandleGetUserByUsername(username);
    }

    public async Task<User?> HandleGetUserByUsernameOrEmail(string usernameOrEmail)
    {
        var user = await _userRepository.HandleGetUserByEmail(usernameOrEmail);
        if (user != null)
            return user;

        return await _userRepository.HandleGetUserByUsername(usernameOrEmail);
    }

    public async Task HandleUpdateUserPassword(User user, string newPassword)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.HandleUpdateUser(user);
    }

    public async Task HandleUpdateUserStatus(string email)
    {
        var user = await HandleGetUserByEmail(email) ?? throw new ExceptionCustom(404, "User not found");
        user.Status = UserStatus.ACTIVE;
        await _userRepository.HandleUpdateUser(user);
    }
}
