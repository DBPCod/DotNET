using StackExchange.Redis;

namespace Backend.Utils.Helpers;

public class RedisHelper
{
    private ConnectionMultiplexer? _connectionMultiplexer;

    public void SetDatabase(ConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public IDatabase GetDatabase()
    {
        if (_connectionMultiplexer == null)
        {
            throw new InvalidOperationException("Redis connection is not initialized.");
        }
        return _connectionMultiplexer.GetDatabase();
    }

    public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        var db = GetDatabase();
        return await db.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        var db = GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task<bool> DeleteKeyAsync(string key)
    {
        var db = GetDatabase();
        return await db.KeyDeleteAsync(key);
    }

    public async Task StoreValue(string key, string value, TimeSpan? expiry = null)
    {
        await SetStringAsync(key, value, expiry);
    }

    public async Task<string?> GetValue(string key)
    {
        return await GetStringAsync(key);
    }
}