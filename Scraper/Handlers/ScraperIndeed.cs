using HtmlAgilityPack;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;

namespace ESOF.WebApp.Scraper.Handlers;

public class ScraperIndeed : IScraper<JobResult>
{
    public async override Task<JobResult> Handle(string url)
    {
        Console.WriteLine("Chegou!");
        var html = await HttpClientHelper.GetHtmlAsync(url);
        return ExtractData(html);

    }

    private JobResult ExtractData(string html)
    {
        if (string.IsNullOrEmpty(html))
        {
            throw new HtmlWebException("HTML vazio ou nulo.");
        }

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var titleNode = htmlDocument.DocumentNode.SelectSingleNode(".//h2[@data-testid='jobsearch-JobInfoHeader-title']");
        var outerHTML = titleNode?.OuterHtml;
        Console.WriteLine(outerHTML);
        var title = titleNode?.InnerHtml.Trim() ?? throw new Exception("Element title not found!");


        return new JobResult("", "", "", "", "");
    }
}

