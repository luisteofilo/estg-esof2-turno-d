using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Experience
{
    [Key] 
    public Guid ExperienceId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public Profile Profile { get; set; }
    
}