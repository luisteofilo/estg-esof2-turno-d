namespace Common.Dtos.Profile;

public class ExperienceDto
{
    public Guid ExperienceId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public DateTime StartDate = DateTime.UtcNow;
    public DateTime EndDate = DateTime.UtcNow;
    public string Description { get; set; }

    public ExperienceDto Clone()
    {
        return new ExperienceDto
        {
            ExperienceId = ExperienceId,
            ProfileId = ProfileId,
            Name = Name,
            CompanyName = CompanyName,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description
        };
    }
}
