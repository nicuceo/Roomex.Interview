using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using System.Text.Json;

namespace Roomex.Interview.Core.Services
{
    public class GeoCodeResolver : IGeoLocationPointResolver
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationFacade _configurationFacade;

        public GeoCodeResolver(IHttpClientFactory httpClientFactory, IConfigurationFacade configurationFacade)
        {
            _httpClientFactory = httpClientFactory;
            _configurationFacade = configurationFacade;
        }

        public async Task<GeoLocationPoint> ResolveAsync(string cityName)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_configurationFacade.GetGeocodeUri()}?q={cityName}");
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Geocode API exception");

            using var contentStream = await response.Content.ReadAsStreamAsync();
            var matchingGeoLocationPoints = await JsonSerializer.DeserializeAsync<GeoLocationPoint[]>(contentStream);
            return matchingGeoLocationPoints?.FirstOrDefault() ?? throw new ArgumentException($"{cityName} geolocation not found.");
        }
    }
}
