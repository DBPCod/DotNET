namespace Backend.Services.Apis;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<List<User>> HandleGetAllUsers()
    {
        return await _userRepository.HandleGetAllUsers();
    }

    public async Task<User?> HandleGetUserById(Guid id)
    {
        return await _userRepository.HandleGetUserById(id);
    }

    public async Task<(List<User> users, int totalCount)> HandleGetUsersWithPagination(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100; // Giới hạn tối đa 100 records per page

        return await _userRepository.HandleGetUsersWithPagination(page, pageSize);
    }

    public async Task<User?> HandleCreateUser(string username, string email, string password, string role, string fullName)
    {
        var existingUserByEmail = await _userRepository.HandleGetUserByEmail(email);
        if (existingUserByEmail != null)
            throw new ExceptionCustom(409, "Email already exists");

        var existingUserByUsername = await _userRepository.HandleGetUserByUsername(username);
        if (existingUserByUsername != null)
            throw new ExceptionCustom(409, "Username already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Username = username,
            FullName = fullName ?? "",
            Email = email,
            Password = hashedPassword,
            Role = string.IsNullOrEmpty(role) ? UserRole.STAFF : Enum.Parse<UserRole>(role),
        };

        return await _userRepository.HandleCreateUser(newUser);
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

    public async Task<bool> HandleDeleteUser(Guid id)
    {
        var user = await _userRepository.HandleGetUserById(id);
        if (user == null)
            throw new ExceptionCustom(404, "User not found");

        return await _userRepository.HandleDeleteUser(id);
    }

    public async Task<List<User>> HandleSearchUsers(string? searchTerm)
    {
        var allUsers = await _userRepository.HandleGetAllUsers();
        
        if (string.IsNullOrEmpty(searchTerm))
            return allUsers;

        searchTerm = searchTerm.ToLower();
        return allUsers.Where(u => 
            u.Username.ToLower().Contains(searchTerm) ||
            u.Email.ToLower().Contains(searchTerm) ||
            u.FullName.ToLower().Contains(searchTerm)
        ).ToList();
    }

    public async Task<User?> HandleUpdateUser(Guid id, string? username, string? email, string? fullName, string? role)
    {
        var user = await _userRepository.HandleGetUserById(id);
        if (user == null)
            throw new ExceptionCustom(404, "User not found");

        // Kiểm tra email trùng lặp (nếu có thay đổi email)
        if (!string.IsNullOrEmpty(email) && email != user.Email)
        {
            var existingUserByEmail = await _userRepository.HandleGetUserByEmail(email);
            if (existingUserByEmail != null)
                throw new ExceptionCustom(409, "Email already exists");
        }

        // Kiểm tra username trùng lặp (nếu có thay đổi username)
        if (!string.IsNullOrEmpty(username) && username != user.Username)
        {
            var existingUserByUsername = await _userRepository.HandleGetUserByUsername(username);
            if (existingUserByUsername != null)
                throw new ExceptionCustom(409, "Username already exists");
        }

        // Cập nhật các field
        if (!string.IsNullOrEmpty(username))
            user.Username = username;
        
        if (!string.IsNullOrEmpty(email))
            user.Email = email;
        
        if (!string.IsNullOrEmpty(fullName))
            user.FullName = fullName;
        
        if (!string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, out var userRole))
            user.Role = userRole;

        return await _userRepository.HandleUpdateUser(user);
    }
}
