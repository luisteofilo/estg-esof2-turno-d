using System.Diagnostics;
using System.Runtime.CompilerServices;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using HtmlAgilityPack;

namespace ESOF.WebApp.Scraper.Handlers;

// Este scraper só não funciona pois o linkedin bloqueia o acesso
public class ProfileScraperLinkedIn //: IScraper<ProfileResult>
{
    /*public async override Task<ProfileResult> Handle(string url)
    {
        var html = await HttpClientHelper.GetHtmlAsync(url);
        return ExtractData(html);
    }
    
    private ProfileResult ExtractData(string html)
    {
        if (string.IsNullOrEmpty(html))
        {
            throw new HtmlWebException("HTML vazio ou nulo.");
        }
        
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var nameNode = htmlDocument.DocumentNode.SelectSingleNode(".//h1[contains(@class, 'text-heading-xlarge')]");
        var name = nameNode?.InnerText.Trim() ?? throw new Exception("Element title not found!");

        var firstname = "placeholder";
        var lastname = "placeholder";
        
        var locationNode = htmlDocument.DocumentNode.SelectSingleNode(".//span[@class='text-body-small inline t-black--light break-words']");
        var location = locationNode?.InnerText.Trim() ?? throw new Exception("Element location not found!");

        var bioNode = htmlDocument.DocumentNode.SelectSingleNode(".//div[@class='text-body-medium break-words']");
        var bio = bioNode?.InnerText.Trim() ?? throw new Exception("Element biography not found!");

        List<ExperienceResult> experiences = new List<ExperienceResult>();
        List<SkillResult> skills = new List<SkillResult>();
        List<EducationResult> educations = new List<EducationResult>();
        
        return new ProfileResult(firstname,lastname,bio,location,experiences,skills,educations);
    }*/

}