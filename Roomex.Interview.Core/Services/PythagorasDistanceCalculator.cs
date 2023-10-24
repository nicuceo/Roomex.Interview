using Roomex.Interview.Core.Constants;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class PythagorasDistanceCalculator : IDistanceCalculator
    {
        public double Calculate(GeoLocationPoint pointA, GeoLocationPoint pointB)
        {
            double longitudeDifference = pointA.Longitude - pointB.Longitude;
            double latitudeDifference = pointA.Latitude - pointB.Latitude;

            var distance = Math.Sqrt(Math.Pow(latitudeDifference, 2) + Math.Pow(longitudeDifference, 2));

            return distance * Calculator.PythagorasFactor;
        }
    }
}
