using System.ComponentModel.DataAnnotations;
using Helpers.Job;
namespace ESOF.WebApp.DBLayer.Entities;

public class Job
{
    [Key]
    public Guid JobId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClientId { get; set; }  // Client who created the Job
    
    public DateTime? EndDate { get; set; }   // End date of the job opportunity
    
    public ICollection<Position>? Positions { get; set; }    // Position of the job

    public CommitmentType? Commitment { get; set; }  // Commitment type of the job

    public RemoteType? Remote { get; set; } // Remote type of the Job

    [Required]
    public String Localization { get; set; }    // Localization of the Job

    public EducationLevel? Education { get; set; }   // Minimum education required

    public ICollection<JobSkill> JobSkills { get; set; }
    
    public String? Experience { get; set; }  // Minimum experience required

    public String? Description { get; set; } // Additional information about the Job
    
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; }
    
}