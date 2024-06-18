using HtmlAgilityPack;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;

namespace ESOF.WebApp.Scraper.Handlers;

public class ScraperWellfound : IScraper<JobResult>
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

        var titleNode = htmlDocument.DocumentNode.SelectSingleNode(".//h1[@class='inline text-xl font-semibold text-black']");
        var title = titleNode?.InnerHtml.Trim() ?? throw new Exception("Element title not found!");

        var locationNode = htmlDocument.DocumentNode.SelectSingleNode(".//a[@class='ml-2 flex items-center text-sm font-normal text-black underline']");
        var location = locationNode?.InnerText.Trim() ?? throw new Exception("Element location not found!");

        var companyNode = htmlDocument.DocumentNode.SelectSingleNode(".//h2[@class='mt-1 text-lg font-bold text-black underline']");
        var company = companyNode?.InnerText.Trim() ?? throw new Exception("Element company not found!");

        var jobInformation = htmlDocument.DocumentNode.SelectSingleNode(".//div[@class='grid grid-cols-1 gap-6 border-b border-gray-300 py-6 md:grid-cols-2']");
        if (jobInformation != null)
        {
            foreach (var div in jobInformation.ChildNodes)
            {
                var otherDetailTitle = "";
                var otherDetailDescription = "";

                if (div.NodeType == HtmlNodeType.Text)
                {
                    otherDetailDescription = div.InnerText.Trim();
                    continue;
                }
                else
                {
                    foreach (var elem in div.ChildNodes)
                    {
                        switch (elem.Name)
                        {
                            case "h3":
                                otherDetailTitle = elem.InnerText.Trim();
                                break;
                            case "span":
                                otherDetailDescription = elem.InnerText.Trim();
                                break;
                            case "string":
                                otherDetailDescription = elem.InnerText.Trim();
                                break;
                            case "div":
                                otherDetailDescription += elem.InnerText.Trim() + " ";
                                break;
                            default:
                                otherDetailDescription = elem.InnerText.Trim();
                                break;
                        }

                    }

                }

                otherDetails += $"{otherDetailTitle}:  {otherDetailDescription} \n";
            }
        }

        var descriptionNode = htmlDocument.DocumentNode.SelectSingleNode(".//div[@class='styles_description__o_yxO inline-block text-black' and @id='job-description']");
        foreach (var elem in descriptionNode.ChildNodes)
        {
            if (elem.NodeType == HtmlNodeType.Text)
            {
                content += elem.InnerText.Trim() + "\n\n";
                continue;
            }

            switch (elem.Name)
            {
                case "p":
                    content += elem.InnerText.Trim() + "\n\n";
                    break;
                case "h3":
                    content += elem.InnerText.Trim() + "\n";
                    break;
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


        return new JobResult(
            title,
            location,
            company,
            content,
            otherDetails
        );
    }
}

