﻿using System.ComponentModel.DataAnnotations;
using Helpers.Job;
namespace ESOF.WebApp.DBLayer.Entities;

public class Job
{
    [Key]
    public Guid JobId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClientId { get; set; }  // Client who created the Job

    public DateTime? EndDate { get; set; }   // End date of the job opportunity

    [Required]
    public string Position { get; set; }    // Position of the job

    public CommitmentType? Commitment { get; set; }  // Commitment type of the job

    public RemoteType? Remote { get; set; } // Remote type of the Job

    [Required]
    public string Localization { get; set; }    // Localization of the Job

    public EducationLevel? Education { get; set; }   // Minimum education required

    public ICollection<JobSkill> JobSkills { get; set; }

    public string? Experience { get; set; }  // Minimum experience required

    public string? Description { get; set; } // Additional information about the Job

    public string? Company { get; set; }

    public string? OtherDetails { get; set; }

    public Import? Import { get; set; }
    public Guid? ImportId { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedAt { get; set; }

}