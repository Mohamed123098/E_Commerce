using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string> GetAsync(string cacheKey)
        {
            var Value = await _database.StringGetAsync(cacheKey);
            return Value.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive)
        {
            var RedisValue =  JsonSerializer.Serialize(cacheValue);
            await _database.StringSetAsync(cacheKey, RedisValue, timeToLive);
        }
    }
}
