namespace Common.Dtos.Profile;

public class EducationDto
{
    public Guid EducationId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string SchoolName { get; set; }
    public DateTime StartDate = DateTime.UtcNow;
    public DateTime EndDate = DateTime.UtcNow;
    
    public EducationDto Clone()
    {
        return new EducationDto
        {
            EducationId = EducationId,
            ProfileId = ProfileId,
            Name = Name,
            SchoolName = SchoolName,
            StartDate = StartDate,
            EndDate = EndDate,
        };
    }
}