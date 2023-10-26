using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class RegionInfoResolver : IRegionInfoResolver
    {
        private readonly ILogger<RegionInfoResolver> _logger;
        private readonly IConfigurationFacade _configurationFacade;

        public RegionInfoResolver(ILogger<RegionInfoResolver> logger, IConfigurationFacade configurationFacade)
        {
            _logger = logger;
            _configurationFacade = configurationFacade;
        }

        public RegionInfo Resolve(string? regionName)
        {
            if (string.IsNullOrEmpty(regionName))
            {
                _logger.LogInformation($"Locale not provided, using the default one.");
                regionName = _configurationFacade.GetDefaultLocale();
            }
            try
            {
                regionName = regionName.Split(';')[0].Split(',')[0].Split('-')[1];
            }
            catch (Exception)
            {
                _logger.LogError($"Accept - Language http header is not valid", regionName);
                throw new ArgumentException("Accept-Language http header is not valid");
            }

            return new RegionInfo(regionName);
        }
    }
}
