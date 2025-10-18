namespace Backend.Initializers;

public class RootUserInitializer(
    ILogger<RootUserInitializer> logger,
    IServiceScopeFactory scopeFactory
    ) : IHostedService
{
    private readonly ILogger<RootUserInitializer> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        var userService = scope.ServiceProvider.GetRequiredService<UserService>();

        string rootEmail = Variable.Enviroments.ROOT_EMAIL;
        string rootPassword = Variable.Enviroments.ROOT_PASSWORD;
        string rootFullname = Variable.Enviroments.ROOT_FULLNAME;

        var user = await userService.HandleCreateUser(rootEmail, rootPassword, "ADMIN", rootFullname);
        if (user != null)
        {
            _logger.LogInformation("Root user created successfully");
        }
        else
        {
            _logger.LogInformation("Root user already exists. Skipping creation.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
