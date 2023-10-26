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
        private readonly ILogger<DistanceController> _logger;
        private readonly IDistanceCalculatorService _distanceCalculatorService;

        public DistanceController(ILogger<DistanceController> logger, IDistanceCalculatorService distanceCalculatorService)
        {
            _logger = logger;
            _distanceCalculatorService = distanceCalculatorService;
        }

        /// <summary>
        /// Calculates distances between two cities using the selected strategy
        /// </summary>
        /// <param name="cityA"></param>
        /// <param name="cityA"></param>
        /// <param name="calculatorStrategy"></param>
        /// <returns>The distance between the provided cities based on ''AcceptLanguage''' header: km or miles</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/distance?cityA=Dublin&amp;cityB=Cleveland&amp;calculatorStrategy=Geodesic
        ///     
        /// </remarks>
        /// <response code="200">Returns the distance between cities</response>
        /// <response code="400">If the provided parameters are not valid</response>
        /// <response code="500">If configuration is missing</response>
        /// <response code="503">If external API's are not available</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [Route("api/v1/distance")]
        public async Task<ActionResult<string>> CalculateAsync([FromQuery] CalculateDistanceRequest request)
        {
            var locale = Request.Headers.AcceptLanguage.FirstOrDefault();
            _logger.LogInformation($"Distance called with locale: {locale}");
            return await _distanceCalculatorService.CalculateAsync(request, locale);
        }
    }
}
