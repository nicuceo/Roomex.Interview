using Roomex.Interview.Core.Constants;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class ImperialLocaleFormatter : ILocaleFormatter
    {
        public string FormatDistance(double distance)
            => $"{distance * Calculator.KmPerMiles} miles";
    }
}
