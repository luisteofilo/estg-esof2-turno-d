using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Dtos.Profile;

public class ExperienceDto
{
    public string? Name { get; set; }
    public string? CompanyName { get; set; }
    public string? Duration { get; set; }
    public string? Description { get; set; }
}