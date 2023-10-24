using Microsoft.AspNetCore.Mvc;
using Roomex.Interview.Api.Filters;
using Roomex.Interview.Core.Models;
using Roomex.Interview.Core.Services.Interfaces;

namespace Roomex.Interview.Api.Controllers.v1
{
    [ApiController]
    [ApiExceptionFilter]
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceCalculatorService _distanceCalculatorService;

        public DistanceController(IDistanceCalculatorService distanceCalculatorService)
        {
            _distanceCalculatorService = distanceCalculatorService;
        }

        [HttpGet]
        [Route("api/v1/distance")]
        public async Task<ActionResult<string>> CalculateAsync([FromQuery] CalculateDistanceRequest request)
        {
            var locale = Request.Headers.AcceptLanguage.FirstOrDefault();
            return await _distanceCalculatorService.CalculateAsync(request, locale);
        }
    }
}
