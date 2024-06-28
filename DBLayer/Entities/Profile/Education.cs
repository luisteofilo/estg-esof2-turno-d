using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Education
{
    [Key]
    public Guid EducationId { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public string SchoolName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Profile Profile { get; set; }
    
}