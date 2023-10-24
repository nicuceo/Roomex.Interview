using System.Globalization;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IRegionInfoResolver
    {
        RegionInfo Resolve(string? regionName);
    }
}
