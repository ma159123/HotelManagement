using HotelManagement.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HotelManagement.Infrastructure.Services
{
    public class MemoryTokenCache : ITokenCache
    {
        private readonly IMemoryCache _cache;

        public MemoryTokenCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task SetAsync(string key, string value, TimeSpan expiry)
        {
            _cache.Set(key, value, expiry);
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            _cache.TryGetValue(key, out string? value);
            return Task.FromResult(value);
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
