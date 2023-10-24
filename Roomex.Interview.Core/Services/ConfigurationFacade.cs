using Microsoft.Extensions.Configuration;
using Roomex.Interview.Core.Constants;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class ConfigurationFacade : IConfigurationFacade
    {
        private readonly IConfiguration _configuration;

        public ConfigurationFacade(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetGeocodeUri() => GetStringConfiguration(ConfigurationKey.GeocodeUri);
        public string GetDefaultLocale() => GetStringConfiguration(ConfigurationKey.DefaultLocale);

        private string GetStringConfiguration(string configurationKey)
            => _configuration[configurationKey]
                     ?? throw new ($"{configurationKey} configuration is missing.");

    }
}
