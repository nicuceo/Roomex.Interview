using System.Text.Json.Serialization;

namespace Roomex.Interview.Core.Models
{
    public record GeoLocationPoint
    {
        [JsonPropertyName("lat")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Longitude { get; set; }
    }
}