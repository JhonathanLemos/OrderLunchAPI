using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lanches.Infra.CacheStorage
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var objectString = await _cache.GetStringAsync(key);

            if (string.IsNullOrWhiteSpace(objectString))
            {
                _logger.LogWarning($"Cache key not found: {key}");
                return default(T);
            }

            _logger.LogInformation($"Cache key found for key: {key}");
            return JsonConvert.DeserializeObject<T>(objectString);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600),
                SlidingExpiration = TimeSpan.FromSeconds(300)
            };

            var objectString = JsonConvert.SerializeObject(data);
            _logger.LogInformation($"Cache set for key: {key}");

            await _cache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
        }
    }
}
