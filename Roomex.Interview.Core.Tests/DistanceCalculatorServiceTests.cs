using Moq;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using Roomex.Interview.Services;
using System.Globalization;

namespace Roomex.Interview.Core.Tests
{
    public class DistanceCalculatorServiceTests
    {
        [Fact]
        public async Task When_Distance_Is_Cached_Returns_Cached_Result()
        {
            // Arrange
            var geoLocationPointResolverMock = new Mock<IGeoLocationPointResolver>();
            var regionInfoResolverMock = new Mock<IRegionInfoResolver>();
            var distanceCalculatorStrategyMock = new Mock<IDistanceCalculatorFactory>();
            var localeFormatterStrategyMock = new Mock<ILocaleFormatterFactory>();
            var distancesCacheMock = new Mock<IDistancesCache>();

            var service = new DistanceCalculatorService(
                geoLocationPointResolverMock.Object,
                regionInfoResolverMock.Object,
                distanceCalculatorStrategyMock.Object,
                localeFormatterStrategyMock.Object,
                distancesCacheMock.Object);

            var request = new CalculateDistanceRequest { CityA = "CityA", CityB = "CityB", CalculatorStrategy = CalculatorStrategy.Pythagorean };
            var locale = "en-US";
            var regionInfo = new RegionInfo("US");
            var cachedResult = "100 miles";

            distancesCacheMock.Setup(x => x.Get(request, It.IsAny<RegionInfo>())).Returns(cachedResult);

            // Act
            var result = await service.CalculateAsync(request, locale);

            // Assert
            Assert.Equal(cachedResult, result);
        }

        [Fact]
        public async Task When_Distance_Is_Not_Cached_Calculates_Result()
        {
            var geoLocationPointResolverMock = new Mock<IGeoLocationPointResolver>();
            geoLocationPointResolverMock.Setup(x => x.ResolveAsync(It.IsAny<string>())).ReturnsAsync(new GeoLocationPoint());

            var regionInfoResolverMock = new Mock<IRegionInfoResolver>();
            regionInfoResolverMock.Setup(x => x.Resolve(It.IsAny<string>())).Returns(new RegionInfo("US"));

            var distanceCalculatorStrategyMock = new Mock<IDistanceCalculatorFactory>();
            var distanceCalculatorMock = new Mock<IDistanceCalculator>();
            distanceCalculatorMock.Setup(x => x.Calculate(It.IsAny<GeoLocationPoint>(), It.IsAny<GeoLocationPoint>())).Returns(100);

            distanceCalculatorStrategyMock.Setup(x => x.GetCalculator(It.IsAny<CalculatorStrategy>())).Returns(distanceCalculatorMock.Object);

            var localeFormatterStrategyMock = new Mock<ILocaleFormatterFactory>();
            var localeFormatterMock = new Mock<ILocaleFormatter>();
            localeFormatterMock.Setup(x => x.FormatDistance(It.IsAny<double>())).Returns("100 miles");

            var distancesCacheMock = new Mock<IDistancesCache>();
            localeFormatterStrategyMock.Setup(x => x.GetLocaleFormatter(It.IsAny<RegionInfo>())).Returns(localeFormatterMock.Object);


            var service = new DistanceCalculatorService(
                geoLocationPointResolverMock.Object,
                regionInfoResolverMock.Object,
                distanceCalculatorStrategyMock.Object,
                localeFormatterStrategyMock.Object,
                distancesCacheMock.Object);

            var request = new CalculateDistanceRequest { CityA = "CityA", CityB = "CityB", CalculatorStrategy = CalculatorStrategy.Pythagorean };

            distancesCacheMock.Setup(x => x.Get(request, It.IsAny<RegionInfo>())).Returns((string?)null);

            var locale = "en-US";

            // Act
            var result = await service.CalculateAsync(request, locale);

            // Assert
            Assert.Equal("100 miles", result);
            distanceCalculatorMock.Verify(x => x.Calculate(It.IsAny<GeoLocationPoint>(), It.IsAny<GeoLocationPoint>()), Times.Once);
        }
    }
}