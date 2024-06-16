using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Handlers;

namespace ESOF.WebApp.Scraper.Helpers;

public class ScraperFactory
{
    private readonly Dictionary<string, IScraper<JobResult>> _scraper = new()
    {
        { "linkedin.com", new ScraperLinkedIn() },
        //{ "indeed.com", new ScraperIndeed() }
    };

    public IScraper<JobResult> CreateScraper(Uri uri)
    {
        var url = uri.OriginalString;

        var scraper = _scraper
            .Where(scraper => url.Contains(scraper.Key))
            .Select(scraper => scraper.Value)
            .FirstOrDefault();

        if (scraper is not null)
            return scraper;

        throw new NotSupportedException("URL não suportado!");
    }
}
