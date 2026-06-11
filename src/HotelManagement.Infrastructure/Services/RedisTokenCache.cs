using HotelManagement.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelManagement.Infrastructure.Services
{
    public class RedisTokenCache : ITokenCache
    {
        private readonly IDistributedCache _cache;

        public RedisTokenCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync(string key, string value, TimeSpan expiry)
        {
            await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            });
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
