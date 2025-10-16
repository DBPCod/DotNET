using StackExchange.Redis;

namespace Backend.Configs;

public class RedisHosted(RedisHelper redisHelper) : IHostedService
{
    private readonly RedisHelper _redisHelper = redisHelper;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var redisHost = Variable.Enviroments.REDIS_HOST;
            var redisPort = Variable.Enviroments.REDIS_PORT;
            var redisUsername = Variable.Enviroments.REDIS_USERNAME;
            var redisPassword = Variable.Enviroments.REDIS_PASSWORD;

            if (string.IsNullOrEmpty(redisHost) ||
                redisPort == 0 ||
                string.IsNullOrEmpty(redisUsername) ||
                string.IsNullOrEmpty(redisPassword))
            {
                Console.WriteLine("⚠️ Redis is not configured in environment variables");
                return;
            }

            var muxer = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints = { { redisHost, redisPort } },
                    User = redisUsername,
                    Password = redisPassword
                }
            );
            var db = muxer.GetDatabase();
            await _redisHelper.SetDatabase(muxer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to Redis: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
