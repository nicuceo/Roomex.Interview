using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Core.Services
{
    public class DistanceCalculatorFactory : IDistanceCalculatorFactory
    {
        private readonly ILogger<DistanceCalculatorFactory> _logger;

        public DistanceCalculatorFactory(ILogger<DistanceCalculatorFactory> logger)
        {
            _logger = logger;
        }

        public IDistanceCalculator GetCalculator(CalculatorStrategy? calculatorStrategy)
            => calculatorStrategy switch
            {
                CalculatorStrategy.Pythagorean => CreatePythagoreanCalculator(),
                CalculatorStrategy.Geodesic => CreateGeodesicCalculator(),
                _ => CreateGeodesicCalculator()
            };

        private IDistanceCalculator CreatePythagoreanCalculator()
        {
            _logger.LogInformation("PythagoreanCalculator will be use to determine the distance.");
            return new PythagorasDistanceCalculator();
        }

        private IDistanceCalculator CreateGeodesicCalculator()
        {
            _logger.LogInformation("GeodesicCalculator will be use to determine the distance.");
            return new GeodesicDistanceCalculator();
        }
    }
}
