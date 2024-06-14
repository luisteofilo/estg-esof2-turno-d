namespace ESOF.WebApp.DBLayer.Entities;

public class JobSkill
{
    public Guid JobId { get; set; }
    public Job Job { get; set; }
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
}