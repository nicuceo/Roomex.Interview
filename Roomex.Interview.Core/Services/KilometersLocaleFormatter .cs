using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class KilometersLocaleFormatter : ILocaleFormatter
    {
        public string FormatDistance(double distance)
            => $"{distance} km";
    }
}
