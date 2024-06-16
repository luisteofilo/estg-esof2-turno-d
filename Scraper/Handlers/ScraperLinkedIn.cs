using HtmlAgilityPack;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;

namespace ESOF.WebApp.Scraper.Handlers;

public class ScraperLinkedIn : IScraper<JobResult>
{
    public async override Task<JobResult> Handle(string url)
    {
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

        string content = string.Empty;
        string otherDetails = string.Empty;

        var titleNode = htmlDocument.DocumentNode.SelectSingleNode(".//h1[@class='top-card-layout__title font-sans text-lg papabear:text-xl font-bold leading-open text-color-text mb-0 topcard__title']");
        var title = titleNode?.InnerHtml.Trim() ?? throw new Exception("Element title not found!");

        var locationNode = htmlDocument.DocumentNode.SelectSingleNode(".//span[@class='topcard__flavor topcard__flavor--bullet']");
        var location = locationNode?.InnerText.Trim() ?? throw new Exception("Element location not found!");

        var companyNode = htmlDocument.DocumentNode.SelectSingleNode(".//a[@class='topcard__org-name-link topcard__flavor--black-link']");
        var company = companyNode?.InnerText.Trim() ?? throw new Exception("Element company not found!");

        var sectionNode = htmlDocument.DocumentNode.SelectSingleNode(".//section[@class='show-more-less-html']");
        var descriptionNode = sectionNode.SelectSingleNode(".//div");
        foreach (var elem in descriptionNode.ChildNodes)
        {
            if (elem.NodeType == HtmlNodeType.Text)
            {
                content += elem.InnerText.Trim() + "\n\n";
                continue;
            }

            switch (elem.Name)
            {
                case "strong":
                    content += elem.InnerText.Trim() + "\n\n";
                    break;
                case "span":
                    content += elem.InnerText.Trim() + "\n";
                    break;
                case "li":
                    content += elem.InnerText.Trim() + "\n";
                    break;
                case "ul":
                case "ol":
                    foreach (var il in elem.ChildNodes)
                    {
                        content += il.InnerText.Trim() + "\n";
                    }
                    content += "\n\n";
                    break;
                default:
                    content += elem.InnerText.Trim() + "\n";
                    break;
            }
        }

        var ulJobDescriptionNode = htmlDocument.DocumentNode.SelectSingleNode(".//ul[@class='description__job-criteria-list']");

        var liNodes = ulJobDescriptionNode.SelectNodes("li");
        foreach (var liNode in liNodes)
        {
            var subtitleNode = liNode.SelectSingleNode(".//h3[@class='description__job-criteria-subheader']");
            var descriptionJob = subtitleNode?.InnerText.Trim() ?? throw new Exception("Element title detail not found!");


            var answerNode = liNode.SelectSingleNode(".//span[@class='description__job-criteria-text description__job-criteria-text--criteria']");
            var descriptionJobDetail = answerNode?.InnerText.Trim() ?? throw new Exception("Element detail not found!");


            otherDetails += $"{descriptionJob}:  {descriptionJobDetail} \n";
        }

        Console.WriteLine("Title: " + title);
        Console.WriteLine("Location: " + location);
        Console.WriteLine("Company: " + company);
        Console.WriteLine("Content: " + content);
        Console.WriteLine("Other Details: " + otherDetails);

        return new JobResult(
            title,
            location,
            company,
            content,
            otherDetails
        );
    }
}
