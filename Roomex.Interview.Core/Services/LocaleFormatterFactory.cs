using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class LocaleFormatterFactory : ILocaleFormatterFactory
    {
        private readonly ILogger<LocaleFormatterFactory> _logger;

        public LocaleFormatterFactory(ILogger<LocaleFormatterFactory> logger)
        {
            _logger = logger;
        }
        public ILocaleFormatter GetLocaleFormatter(RegionInfo regionInfo)
            => regionInfo.IsMetric ?
            CreateKilometersFormatter() :
            CreateImperialFormatter();

        private ILocaleFormatter CreateImperialFormatter()
        {
            _logger.LogInformation("ImperialLocaleFormatter will be used to format the result.");
            return new ImperialLocaleFormatter();
        }

        private ILocaleFormatter CreateKilometersFormatter()
        {
            _logger.LogInformation("KilometersLocaleFormatter will be used to format the result.");
            return new KilometersLocaleFormatter();
        }

    }
}
