namespace Common.Dtos.Job;

public class DashboardJobDTo
{
    public Guid JobId { get; set; }
    public Guid SkillId { get; set; }
    public string? SkillName { get; set; } 
    public bool IsRequired { get; set; }
}