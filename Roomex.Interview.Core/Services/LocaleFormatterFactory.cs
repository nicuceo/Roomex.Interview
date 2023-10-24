using Roomex.Interview.Core.Services.Interfaces;
using System.Globalization;

namespace Roomex.Interview.Core.Services
{
    public class LocaleFormatterFactory : ILocaleFormatterFactory
    {
        public ILocaleFormatter GetLocaleFormatter(RegionInfo regionInfo)
            => regionInfo.IsMetric ?
            new KilometersLocaleFormatter() :
            new ImperialLocaleFormatter();

    }
}
