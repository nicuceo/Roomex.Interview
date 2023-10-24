using Moq;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using Roomex.Interview.Core.Services;
using System.Net;
using System.Text.Json;

namespace Roomex.Interview.Core.Tests
{
    public class GeoCodeResolverTests
    {
        [Fact]
        public async Task When_Receiving_Location_Successfully_Returns_GeoLocationPoint()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            var httpClientMock = new Mock<HttpClient>();

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
            configurationFacadeMock.Setup(x => x.GetGeocodeUri()).Returns("https://geocode.maps.co/search");

            var resolver = new GeoCodeResolver(httpClientFactoryMock.Object, configurationFacadeMock.Object);

            var cityName = "Dublin";
            var expectedGeoLocationPoint = new GeoLocationPoint { Latitude = 53.3497645, Longitude = -6.2602732 };

            var successResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(new[] { expectedGeoLocationPoint }))
            };

            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), CancellationToken.None))
                .ReturnsAsync(successResponse);

            // Act
            var result = await resolver.ResolveAsync(cityName);

            // Assert
            Assert.Equal(expectedGeoLocationPoint, result);
        }

        [Fact]
        public async Task When_Receiving_Unsuccessful_Response_From_Geocode_Throws_Exception()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            var httpClientMock = new Mock<HttpClient>();

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
            configurationFacadeMock.Setup(x => x.GetGeocodeUri()).Returns("https://geocode.maps");

            var resolver = new GeoCodeResolver(httpClientFactoryMock.Object, configurationFacadeMock.Object);

            var cityName = "Dublin";

            var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), CancellationToken.None))
                .ReturnsAsync(errorResponse);

            // Act and Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => resolver.ResolveAsync(cityName));
        }

        [Fact]
        public async Task When_Receiving_No_Matching_GeoLocation_Points_Throws_Exception()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationFacadeMock = new Mock<IConfigurationFacade>();
            var httpClientMock = new Mock<HttpClient>();

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
            configurationFacadeMock.Setup(x => x.GetGeocodeUri()).Returns("https://geocode.maps.co/search");

            var resolver = new GeoCodeResolver(httpClientFactoryMock.Object, configurationFacadeMock.Object);

            var cityName = "NonExistentCity";

            var successResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(new GeoLocationPoint[0]))
            };

            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), CancellationToken.None))
                .ReturnsAsync(successResponse);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => resolver.ResolveAsync(cityName));
        }
    }
}
