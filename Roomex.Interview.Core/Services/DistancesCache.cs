using Microsoft.Extensions.Caching.Memory;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class DistancesCache : IDistancesCache
    {
        private readonly IMemoryCache _memoryCache;

        public DistancesCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public string? Get(CalculateDistanceRequest request, RegionInfo regionInfo)
        {
            var cacheKey = GetCacheKey(request, regionInfo);
            return _memoryCache.Get<string>(cacheKey);
        }

        public void Set(CalculateDistanceRequest request, RegionInfo regionInfo, string value)
        {
            var cacheKey = GetCacheKey(request, regionInfo);
            _memoryCache.Set(cacheKey, value);
        }


        private static string GetCacheKey(CalculateDistanceRequest request, RegionInfo regionInfo)
        {
            var cities = new List<string> { request.CityA!, request.CityB! };
            cities.Sort();
            return $"{string.Join('-', cities)}-{regionInfo.Name}";
        }
    }
}
