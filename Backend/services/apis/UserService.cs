namespace Backend.Services.Apis;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

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
}
