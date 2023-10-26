using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Constants;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class ConfigurationFacade : IConfigurationFacade
    {
        private readonly ILogger<ConfigurationFacade> _logger;
        private readonly IConfiguration _configuration;

        public ConfigurationFacade(ILogger<ConfigurationFacade> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string GetGeocodeUri() => GetStringConfiguration(ConfigurationKey.GeocodeUri);
        public string GetDefaultLocale() => GetStringConfiguration(ConfigurationKey.DefaultLocale);

        private string GetStringConfiguration(string configurationKey)
        {
            var configurationValue = _configuration[configurationKey];
            if (configurationValue is null)
            {
                _logger.LogError($"Configuration is missing: {configurationKey}");
                throw new($"{configurationKey} configuration is missing.");
            }
            return configurationValue;
        }

    }
}
