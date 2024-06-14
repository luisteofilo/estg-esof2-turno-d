using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Interview
{
    [Key]
    public Guid InterviewId { get; set; }
    
    [Key]
    public Guid CandidateId { get; set; }
    
    [Required]
    public DateTime DateHour { get; set; }
    
    [Required]
    public InterviewState InterviewState { get; set; }
}