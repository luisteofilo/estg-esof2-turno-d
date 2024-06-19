namespace Common.Dtos.Profile;

public class EducationDto
{
    public Guid EducationId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string SchoolName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    
    
}