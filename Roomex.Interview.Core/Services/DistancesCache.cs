using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class DistancesCache : IDistancesCache
    {
        private readonly ILogger<DistancesCache> _logger;
        private readonly IMemoryCache _memoryCache;

        public DistancesCache(ILogger<DistancesCache> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }
        public string? Get(CalculateDistanceRequest request, RegionInfo regionInfo)
        {
            var cacheKey = GetCacheKey(request, regionInfo);
            _logger.LogInformation($"Rreading {cacheKey} from cache.");
            return _memoryCache.Get<string>(cacheKey);
        }

        public void Set(CalculateDistanceRequest request, RegionInfo regionInfo, string value)
        {
            var cacheKey = GetCacheKey(request, regionInfo);
            _memoryCache.Set(cacheKey, value);
            _logger.LogInformation($"{cacheKey} added to cache.");
        }


        private static string GetCacheKey(CalculateDistanceRequest request, RegionInfo regionInfo)
        {
            var cities = new List<string> { request.CityA!, request.CityB! };
            cities.Sort();
            return $"{string.Join('-', cities)}-{regionInfo.Name}";
        }
    }
}
