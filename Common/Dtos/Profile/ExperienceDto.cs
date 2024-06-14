namespace Common.Dtos.Profile;

public class ExperienceDto
{
    public Guid ExperienceId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Duration { get; set; }
    public string Description { get; set; }
}