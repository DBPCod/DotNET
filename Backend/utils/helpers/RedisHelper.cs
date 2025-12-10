using System.Collections.Concurrent;
using StackExchange.Redis;

namespace Backend.Utils.Helpers;

public class RedisHelper
{
    private ConnectionMultiplexer? _connectionMultiplexer;
    // Fallback lưu tạm trong bộ nhớ khi Redis chưa được cấu hình/khởi tạo
    private static readonly ConcurrentDictionary<string, (string Value, DateTimeOffset? ExpireAt)> _memoryStore = new();

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
        if (_connectionMultiplexer == null)
        {
            var expireAt = expiry.HasValue ? DateTimeOffset.UtcNow.Add(expiry.Value) : (DateTimeOffset?)null;
            _memoryStore[key] = (value, expireAt);
            return true;
        }

        var db = GetDatabase();
        return await db.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        if (_connectionMultiplexer == null)
        {
            if (_memoryStore.TryGetValue(key, out var entry))
            {
                if (entry.ExpireAt.HasValue && entry.ExpireAt.Value < DateTimeOffset.UtcNow)
                {
                    _memoryStore.TryRemove(key, out _);
                    return null;
                }

                return entry.Value;
            }

            return null;
        }

        var db = GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task<bool> DeleteKeyAsync(string key)
    {
        if (_connectionMultiplexer == null)
        {
            return _memoryStore.TryRemove(key, out _);
        }

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