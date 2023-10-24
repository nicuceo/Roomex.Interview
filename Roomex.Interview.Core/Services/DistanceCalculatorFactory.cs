using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class DistanceCalculatorFactory : IDistanceCalculatorFactory
    {
        public IDistanceCalculator GetCalculator(CalculatorStrategy? calculatorStrategy)
            => calculatorStrategy switch
            {
                CalculatorStrategy.Pythagorean => new PythagorasDistanceCalculator(),
                CalculatorStrategy.Geodesic => new GeodesicDistanceCalculator(),
                _ => new GeodesicDistanceCalculator()
            };
    }
}
