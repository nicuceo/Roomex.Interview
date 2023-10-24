using Roomex.Interview.Core.Constants;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class GeodesicDistanceCalculator : IDistanceCalculator
    {
        public double Calculate(GeoLocationPoint pointA, GeoLocationPoint pointB)
        {
            double a = ToRadians(90 - pointB.Latitude);
            double b = ToRadians(90 - pointA.Latitude);
            double phi = ToRadians(pointA.Longitude - pointB.Longitude);

            double cosP = Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(phi);

            return Math.Acos(cosP) * Calculator.EarthRadius;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
