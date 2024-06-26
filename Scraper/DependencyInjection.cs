using ESOF.WebApp.Scraper.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ESOF.WebApp.Scraper;

public static class DependencyInjection
{
    public static IServiceCollection AddScraperDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ScraperFactory>();

        return services;
    }
}

