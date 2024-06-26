using System.Runtime.InteropServices.JavaScript;

namespace ESOF.WebApp.Scraper.Contracts;


public record ExperienceResult(
    string Name,
    string CompanyName,
    DateTime StartDate,
    DateTime EndDate,
    string Description
);

public record SkillResult(
    string Name
);

public record EducationResult(
    string Name,
    string SchoolName,
    DateTime StartDate,
    DateTime EndDate
);

public record ProfileResult
(
    string FirstName,
    string LastName,
    string Bio,
    string Location
    /*List<ExperienceResult> experiences
    List<SkillResult> skills, 
    List<EducationResult> educations*/
);