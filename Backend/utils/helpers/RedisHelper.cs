using StackExchange.Redis;

namespace Backend.Utils.Helpers
{
    public class RedisHelper
    {
        private ConnectionMultiplexer? _connection;
        public IDatabase Database = null!;

        public async Task SetDatabase(
            ConnectionMultiplexer connection)
        {
            _connection = connection;
            Database = connection.GetDatabase();
        }

        public async Task StoreValue(string key, string value, TimeSpan timeSpan)
        {
            try
            {
                await Database.StringSetAsync(key, value, timeSpan);
            }
            catch (Exception ex)
            {
                throw new ExceptionCustom(ex.Message);
            }
        }

        public async Task<string> GetValue(string key)
        {
            try
            {
                if (_connection == null)
                {
                    Console.WriteLine($"❌ Redis connection is null when getting key: {key}");
                    throw new ExceptionCustom("Redis connection is not established");
                }

                if (!_connection.IsConnected)
                {
                    Console.WriteLine($"❌ Redis connection is not connected when getting key: {key}");
                    throw new ExceptionCustom("Redis connection is not connected");
                }

                Console.WriteLine($"🔍 Getting Redis key: {key}");
                var redisValue = await Database.StringGetAsync(key);
                Console.WriteLine($"📄 Redis key {key} value: {(redisValue.IsNullOrEmpty ? "empty" : "found")}");

                return redisValue.IsNullOrEmpty ? "" : redisValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Redis GetValue error for key {key}: {ex.Message}");
                throw new ExceptionCustom(ex.Message);
            }
        }
    }
}
