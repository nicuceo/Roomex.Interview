using Roomex.Interview.Core.Models;
using System.Globalization;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IDistancesCache
    {
        string? Get(CalculateDistanceRequest request, RegionInfo regionInfo);
        void Set(CalculateDistanceRequest request, RegionInfo regionInfo, string value);
    }
}
