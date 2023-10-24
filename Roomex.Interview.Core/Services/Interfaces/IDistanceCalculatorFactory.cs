using Roomex.Interview.Core.Models;

namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IDistanceCalculatorFactory
    {
        IDistanceCalculator GetCalculator(CalculatorStrategy? calculatorStrategy);
    }
}
