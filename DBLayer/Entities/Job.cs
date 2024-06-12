using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public enum CommitmentType
{
    Hourly,
    PartTime,
    FullTime
}

public enum RemoteType
{
    Hybrid,
    Home,
    Office
}

public class Job
{
    [Key]
    public Guid JobId { get; set; }
    
    //public Client Client { get; set; }    // Client who created the Job
    
    [Required]
    public String Salary { get; set; }
    
    [Required]
    public CommitmentType Commitment { get; set; }  // Commitment type of the job
    
    [Required]
    public RemoteType Remote { get; set; } // Remote type of the Job
    
    [Required]
    public String Localization { get; set; }    // Localization of the Job
    
    [Required]
    public String Education { get; set; }   // Minimum education required
    
    //public ICollection<Skill> RequiredSkills { get; set; }
    
    //public ICollection<Skill> NiceToHaveSkills { get; set; }
    
    [Required]
    public String experience { get; set; }  // Minimum experience required
    
    [Required]
    public String language { get; set; } // Speaking language required
    
    public String description { get; set; } // Additional information about the Job
    
}