using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Dtos.Profile;

public class ProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public string? Location { get; set; }
    public string? UrlBackground { get; set; }
    public string? UrlProfile { get; set; }
    
    public List<string> Skills { get; set; }
    
    public List<ExperienceDto> Experiences { get; set; }
    
    public List<EducationDto> Educations { get; set; }
}