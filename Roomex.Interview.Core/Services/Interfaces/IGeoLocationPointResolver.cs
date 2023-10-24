using Roomex.Interview.Core.Models;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IGeoLocationPointResolver
    {
        Task<GeoLocationPoint> ResolveAsync(string cityName);
    }
}
