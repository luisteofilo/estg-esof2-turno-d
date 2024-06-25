using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities.Interviews;

public class Interview
{
    //Sempre que Uma entrevista é criada o estado é automaticamente atribuido
    //Assim quando marcamos a entrevista fica com o estado Scheduled
    public Interview()
    {
        InterviewState = InterviewState.Scheduled;
    }

    [Required]
    public Guid CandidateId { get; set; }
        
    [Required]
    public Guid InterviewerId { get; set; }
        
    [Key]
    public Guid InterviewId { get; set; }
        
    [Required]
    public InterviewState InterviewState { get; set; } 
                
    [Required]
    public string Location { get; set; }
        
    [Required]
    public DateTime DateHourStart { get; set; } 
        
    [Required]
    public DateTime DateHourEnd { get; set; } 
        
    [Required]
    public bool PresenceMarked { get; set; }
        
    public Candidate Candidate { get; set; }
        
    public Interviewer Interviewer { get; set; }
    }
