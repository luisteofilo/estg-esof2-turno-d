using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Profile
{
    [Key]
    public Guid ProfileId { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public string? Location { get; set; }
    public string? UrlProfile { get; set; }
    
    public User User { get; set; }
    public Talent Talent { get; set; }
    public ICollection<ProfileSkill> ProfileSkills { get; set; }
    public ICollection<Education> Educations { get; set; }
    public ICollection<Experience> Experiences { get; set; }
}