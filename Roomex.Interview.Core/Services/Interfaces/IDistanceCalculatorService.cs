using Roomex.Interview.Core.Models;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IDistanceCalculatorService
    {
        Task<string> CalculateAsync(CalculateDistanceRequest request, string? locale);
    }
}