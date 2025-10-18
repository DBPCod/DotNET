namespace Backend.Services.Apis;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<User?> HandleCreateUser(string email, string password, string role, string FullName)
    {
        var existingUser = await _userRepository.HandleGetUserByEmail(email);
        if (existingUser != null)
            return null;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            FullName = FullName ?? "",
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

    public async Task HandleUpdateUserPassword(User user, string newPassword)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.HandleUpdateUser(user);
    }
}
