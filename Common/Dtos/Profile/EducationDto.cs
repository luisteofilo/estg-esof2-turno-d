using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Profile;

public class EducationDto
{
    public string? Name { get; set; }
    public string? SchoolName { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}