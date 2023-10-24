using Roomex.Interview.Core.Models;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IDistanceCalculator
    {
        double Calculate(GeoLocationPoint pointA, GeoLocationPoint pointB);
    }
}
