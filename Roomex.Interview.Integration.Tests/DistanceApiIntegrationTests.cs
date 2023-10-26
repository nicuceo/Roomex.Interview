using Microsoft.AspNetCore.Mvc.Testing;

namespace Roomex.Interview.Integration.Tests
{
    public class DistanceApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_No_Locale_And_No_Calculator_Strategy_Returns_Correct_Result()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/distance?cityA=Dublin&cityB=Cleveland");

            // Assert
            response.EnsureSuccessStatusCode(); 
            Assert.Equal("5549,20271051316 km", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_No_Locale_And_With_Geodesic_Strategy_Returns_Correct_Result()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/distance?cityA=Dublin&cityB=Cleveland");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("5549,20271051316 km", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_No_Locale_And_With_Pythagorean_Strategy_Returns_Correct_Result()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/distance?cityA=Dublin&cityB=Cleveland&calculatorStrategy=Pythagorean");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("8475,796118096265 km", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_Imperial_Locale_Returns_Correct_Result_In_Miles()
        {
            // Arrange
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/distance?cityA=Dublin&cityB=Cleveland");
            request.Headers.Add("Accept-Language", "en-US");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("3448,1147028811934 miles", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_Non_Imperial_Locale_Returns_Correct_Result_In_Kilometers()
        {
            // Arrange
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/distance?cityA=Dublin&cityB=Cleveland");
            request.Headers.Add("Accept-Language", "de-DE");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("5549,20271051316 km", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Valid_Cities_With_Invalid_Locale_Returns_Error()
        {
            // Arrange
            var client = CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/distance?cityA=Dublin&cityB=Cleveland");
            request.Headers.Add("Accept-Language", "NotValidLocale");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("{\"message\":\"Accept-Language http header is not valid\"}", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_With_Invalid_City_Returns_Error()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/distance?cityA=InvalidCity&cityB=Cleveland");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("{\"message\":\"InvalidCity geolocation not found.\"}", await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task When_Calling_WithoutRequired_Params_Invalid_City_Returns_Error()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/distance?cityA=Dublin");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var respon = await response.Content.ReadAsStringAsync();
            Assert.Contains("errors\":{\"cityB\":[\"A value for the 'cityB' parameter or property was not provided.\"]}", await response.Content.ReadAsStringAsync());
        }


        private static HttpClient CreateClient()
        {
            var factory = new WebApplicationFactory<Program>();
            return factory.CreateClient();
        }
    }
}