using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities.FAQ;

public class Job
{
    [Key]
    public Guid JobId { get; set; }
    
    [Required]
    public string JobTitle { get; set; }
}