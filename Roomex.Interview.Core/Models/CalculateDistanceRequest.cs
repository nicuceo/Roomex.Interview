using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Roomex.Interview.Core.Models
{
    public record CalculateDistanceRequest
    {
        [BindRequired]
        [FromQuery(Name = "cityA")]
        public string? CityA { get; set; }

        [BindRequired]
        [FromQuery(Name = "cityB")]
        public string? CityB { get; set; }

        [FromQuery(Name = "calculatorStrategy")]
        public CalculatorStrategy? CalculatorStrategy { get; set; }
    }
}
