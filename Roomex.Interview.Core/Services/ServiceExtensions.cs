using Microsoft.Extensions.DependencyInjection;
using Roomex.Interview.Core.Services.Interfaces;
using Roomex.Interview.Services;

namespace Roomex.Interview.Core.Services
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();
            services.AddScoped<IGeoLocationPointResolver, GeoCodeResolver>();
            services.AddScoped<IConfigurationFacade, ConfigurationFacade>();
            services.AddScoped<IRegionInfoResolver, RegionInfoResolver>();
            services.AddScoped<IDistanceCalculatorFactory, DistanceCalculatorFactory>();
            services.AddScoped<ILocaleFormatterFactory, LocaleFormatterFactory>();
            services.AddScoped<IDistancesCache, DistancesCache>();
        }
    }
}
