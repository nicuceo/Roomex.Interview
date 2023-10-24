using Microsoft.Extensions.Caching.Memory;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Services
{
    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        private readonly IGeoLocationPointResolver _geoLocationPointResolver;
        private readonly IRegionInfoResolver _regionInfoResolver;
        private readonly IDistanceCalculatorFactory _distanceCalculatorStrategy;
        private readonly ILocaleFormatterFactory _localeFormatterStrategy;
        private readonly IDistancesCache _distancesCache;

        public DistanceCalculatorService(
            IGeoLocationPointResolver geoLocationPointResolver,
            IRegionInfoResolver regionInfoResolver,
            IDistanceCalculatorFactory distanceCalculatorStrategy,
            ILocaleFormatterFactory localeFormatterStrategy,
            IDistancesCache distancesCache)
        {
            _geoLocationPointResolver = geoLocationPointResolver;
            _regionInfoResolver = regionInfoResolver;
            _distanceCalculatorStrategy = distanceCalculatorStrategy;
            _localeFormatterStrategy = localeFormatterStrategy;
            _distancesCache = distancesCache;
        }
        public async Task<string> CalculateAsync(CalculateDistanceRequest request, string? locale)
        {
            var regionInfo = _regionInfoResolver.Resolve(locale);
            var result = _distancesCache.Get(request, regionInfo);

            if (result != null)
                return result!;

            var pointA = await _geoLocationPointResolver.ResolveAsync(request.CityA!);
            var pointB = await _geoLocationPointResolver.ResolveAsync(request.CityB!);

            var distanceCalculator = _distanceCalculatorStrategy.GetCalculator(request.CalculatorStrategy);
            var distance = distanceCalculator.Calculate(pointA, pointB);

            var localeFormatter = _localeFormatterStrategy.GetLocaleFormatter(regionInfo);
            result = localeFormatter.FormatDistance(distance);

            _distancesCache.Set(request, regionInfo, result);
            return result;
        }


    }
}