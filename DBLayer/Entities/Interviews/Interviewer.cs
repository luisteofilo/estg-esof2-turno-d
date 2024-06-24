using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities.Interviews;
public class Interviewer
{
    [Key]
    public Guid InterviewerId { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required] 
    public ICollection<Interview> Interviews { get; set; }
}