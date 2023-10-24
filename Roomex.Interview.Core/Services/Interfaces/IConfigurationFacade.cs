namespace Roomex.Interview.Core.Services.Interfaces
{
    public interface IConfigurationFacade
    {
        string GetGeocodeUri();
        string GetDefaultLocale();
    }
}
