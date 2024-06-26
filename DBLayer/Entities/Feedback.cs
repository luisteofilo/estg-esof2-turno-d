
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESOF.WebApp.DBLayer.Entities;

public class Feedback
{
    [Key]
    public Guid FeedbackId { get; set; }
    
    public string Message { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public Guid InterviewId { get; set; }
    
    public Interview Interview { get; set; }

}
