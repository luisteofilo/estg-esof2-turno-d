using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Skill
{
    [Key]
    public Guid SkillId { get; set; }
    public string Name { get; set; } 
    
    
}