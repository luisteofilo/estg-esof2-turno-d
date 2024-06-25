using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Skill
{
    [Key]
    public Guid SkillId { get; set; }
    public string Name { get; set; }
    public ICollection<ProfileSkill> ProfileSkills { get; set; }
    
    public ICollection<JobSkill> JobSkills { get; set; }

    public IQueryable RequiredJobSkills => JobSkills.Where(js => js.IsRequired).AsQueryable();
    
    public IQueryable NtHJobSkills => JobSkills.Where(js => !js.IsRequired).AsQueryable();
    
}