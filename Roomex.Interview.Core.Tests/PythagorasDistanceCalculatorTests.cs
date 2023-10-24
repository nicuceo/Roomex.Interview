using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services;

namespace Roomex.Interview.Core.Tests
{
    public class PythagorasDistanceCalculatorTests
    {
        [Fact]
        public void When_Same_Point_Is_Provided_Returns_Zero()
        {
            // Arrange
            var calculator = new PythagorasDistanceCalculator();
            var point = new GeoLocationPoint { Latitude = 10.0, Longitude = 20.0 };

            // Act
            var distance = calculator.Calculate(point, point);

            // Assert
            Assert.True(Math.Abs(distance) < 0.0001);
        }

        [Fact]
        public void When_Dublin_And_Cleveland_Are_Provided_Returns_Actual_Distance()
        {
            // Arrange
            var calculator = new PythagorasDistanceCalculator();
            var dublin = new GeoLocationPoint { Latitude = 53.297975, Longitude = -6.372663 };
            var cleveland = new GeoLocationPoint { Latitude = 41.385101, Longitude = -81.440440 };

            // Act
            var distance = calculator.Calculate(dublin, cleveland);

            // Assert
            Assert.Equal(8436.79427763067, distance);
        }
    }
}
