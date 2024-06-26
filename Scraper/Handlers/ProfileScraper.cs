using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using HtmlAgilityPack;

namespace ESOF.WebApp.Scraper.Handlers;

public class ProfileScraper : IScraper<ProfileResult>
{
    public async override Task<ProfileResult> Handle(string url)
    {
        var html = await HttpClientHelper.GetHtmlAsync(url);
        return ExtractData(html);
    }

    private ProfileResult ExtractData(string html)
    {
        if (string.IsNullOrEmpty(html))
        {
            throw new HtmlWebException("HTML vazio ou nulso.");
        }

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var nameNode = htmlDocument.DocumentNode.SelectSingleNode(".//h2[@id='name']");
        var name = nameNode?.InnerText.Trim() ?? throw new Exception("Element title not found!");

        var tempName = name.Split(" ");
        var firstname = tempName[0];
        var lastname = tempName[1];

        var bioNode = htmlDocument.DocumentNode.SelectSingleNode(".//p[@id='bio']");
        var bio = bioNode?.InnerText.Trim() ?? throw new Exception("Element biography not found!");

        var locationNode = htmlDocument.DocumentNode.SelectSingleNode(".//p[@id='location']");
        var location = locationNode?.InnerText.Trim() ?? throw new Exception("Element location not found!");

        /*var experiences = ExtractExperiences(htmlDocument);
        var skills = ExtractSkills(htmlDocument);
        var educations = ExtractEducations(htmlDocument);*/
        return new ProfileResult(firstname, lastname, bio, location/*,experiences, skills, educations*/);
    }

    /*private List<ExperienceResult> ExtractExperiences(HtmlDocument htmlDocument)
    {
        var experiences = new List<ExperienceResult>();
        var experienceElements = htmlDocument.DocumentNode.SelectNodes("//div[@id='experiences']/div");

        if (experienceElements != null)
        {
            foreach (var experienceElement in experienceElements)
            {
                var nameNode = experienceElement.SelectSingleNode(".//h4[@class='h6 title']");
                var companyNameNode = experienceElement.SelectSingleNode(".//p[@class='text-muted company']");
                var startDateNode = experienceElement.SelectSingleNode(".//p[@class='text-muted startdate']");
                var endDateNode = experienceElement.SelectSingleNode(".//p[@class='text-muted enddate']");

                var description = "description placeholder";

                if (nameNode != null && companyNameNode != null && startDateNode != null && endDateNode != null)
                {
                    var name = nameNode.InnerText.Trim();
                    var companyName = companyNameNode.InnerText.Trim();

                    var startDateString = startDateNode.InnerText.Trim();
                    var endDateString = endDateNode.InnerText.Trim();


                    var startDate = DateTime.ParseExact(startDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(endDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    experiences.Add(new ExperienceResult(name, companyName, startDate, endDate, description));
                }
            }
        }

        return experiences;
    }

    private List<SkillResult> ExtractSkills(HtmlDocument htmlDocument)
    {
        var skills = new List<SkillResult>();
        var skillElements = htmlDocument.DocumentNode.SelectNodes("//div[@id='skills']/div");

        foreach (var skillElement in skillElements)
        {

            var skillName = skillElement.SelectSingleNode(".//div[@class='bg-light p-2 rounded mr-2 mb-2']")?.InnerText.Trim() ?? "";

            skills.Add(new SkillResult(skillName));
        }


        return skills;
    }

    private List<EducationResult> ExtractEducations(HtmlDocument htmlDocument)
    {
        var educations = new List<EducationResult>();
        var educationElements = htmlDocument.DocumentNode.SelectNodes("//div[@id='educations']/div");

        if (educationElements != null)
        {
            foreach (var educationElement in educationElements)
            {
                var nameNode = educationElement.SelectSingleNode(".//h4[@class='h6 school']");
                var schoolNameNode = educationElement.SelectSingleNode(".//p[@class='text-muted name']");
                var startDateNode = educationElement.SelectSingleNode(".//p[@class='text-muted startdate']");
                var endDateNode = educationElement.SelectSingleNode(".//p[@class='text-muted enddate']");

                if (nameNode != null && schoolNameNode != null && startDateNode != null && endDateNode != null)
                {
                    var name = nameNode.InnerText.Trim();
                    var schoolName = schoolNameNode.InnerText.Trim();

                    var startDateString = startDateNode.InnerText.Trim();
                    var endDateString = endDateNode.InnerText.Trim();


                    var startDate = DateTime.ParseExact(startDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(endDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    educations.Add(new EducationResult(name, schoolName, startDate, endDate));
                }
            }
        }

        return educations;
    }*/
}