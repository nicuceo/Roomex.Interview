using System.Globalization;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface ILocaleFormatterFactory
    {
        ILocaleFormatter GetLocaleFormatter(RegionInfo regionInfo);
    }
}
