using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class RegionInfoResolver : IRegionInfoResolver
    {
        private readonly IConfigurationFacade _configurationFacade;

        public RegionInfoResolver(IConfigurationFacade configurationFacade)
        {
            _configurationFacade = configurationFacade;
        }

        public RegionInfo Resolve(string? regionName)
        {
            if (string.IsNullOrEmpty(regionName))
            {
                regionName = _configurationFacade.GetDefaultLocale();
            }
            try
            {
                regionName = regionName.Split(';')[0].Split(',')[0].Split('-')[1];
            }
            catch (Exception)
            {
                throw new ArgumentException("Accept-Language http header is not valid");
            }

            return new RegionInfo(regionName);
        }
    }
}
