using Microsoft.Extensions.Logging;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;
using System.Text.Json;

namespace Roomex.Interview.Core.Services
{
    public class GeoCodeResolver : IGeoLocationPointResolver
    {
        private readonly ILogger<GeoCodeResolver> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfigurationFacade _configurationFacade;

        public GeoCodeResolver(ILogger<GeoCodeResolver> logger, IHttpClientFactory httpClientFactory, IConfigurationFacade configurationFacade)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configurationFacade = configurationFacade;
        }

        public async Task<GeoLocationPoint> ResolveAsync(string cityName)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = $"{_configurationFacade.GetGeocodeUri()}?q={cityName}";
            _logger.LogInformation($"Requesting geo location point from: {uri}");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Geocode responded with error");
                throw new HttpRequestException("Geocode API exception");
            }

            using var contentStream = await response.Content.ReadAsStreamAsync();
            var matchingGeoLocationPoints = await JsonSerializer.DeserializeAsync<GeoLocationPoint[]>(contentStream);
            var mostAccuratePoint = matchingGeoLocationPoints?.FirstOrDefault();

            if (mostAccuratePoint is null)
            {
                _logger.LogError($"City {cityName} could not be found");
                throw new ArgumentException($"{cityName} geolocation not found.");
            }

            return mostAccuratePoint;
        }
    }
}
