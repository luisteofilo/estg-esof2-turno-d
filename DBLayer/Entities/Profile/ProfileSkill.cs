namespace ESOF.WebApp.DBLayer.Entities;

public class ProfileSkill
{
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
}