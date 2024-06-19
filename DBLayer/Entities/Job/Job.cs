using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public enum CommitmentType
{
    Hourly,
    PartTime,
    FullTime
}

public enum EducationLevel
{
    PrimaryEducation,
    SecondaryEducation,
    Bachelors,
    Masters,
    Doctoral
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

    [Required]
    public Guid ClientId { get; set; }
    public Client Client { get; set; }    // Client who created the Job

    [Required]
    public DateTime EndDate { get; set; }   // End date of the job opportunity

    [Required]
    public String Position { get; set; }    // Position of the job

    [Required]
    public CommitmentType? Commitment { get; set; }  // Commitment type of the job

    [Required]
    public RemoteType? Remote { get; set; } // Remote type of the Job

    [Required]
    public String Localization { get; set; }    // Localization of the Job

    [Required]
    public EducationLevel? Education { get; set; }   // Minimum education required

    public ICollection<JobSkill> JobSkills { get; set; }
    
    public IQueryable RequiredSkills => JobSkills.Where(js => js.IsRequired).AsQueryable();
    
    public IQueryable NiceToHaveSkills => JobSkills.Where(js => !js.IsRequired).AsQueryable();

    [Required]
    public String Experience { get; set; }  // Minimum experience required

    [Required]
    public String Language { get; set; } // Speaking language required

    public String? Description { get; set; } // Additional information about the Job
}