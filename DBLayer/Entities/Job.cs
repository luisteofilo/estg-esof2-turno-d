using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Job
{
    [Key]
    public Guid JobId { get; init; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public string Company { get; set; } = null!;

    [Required]
    public string OtherDetails { get; set; } = null!;

    public Import? Import { get; set; }
    public Guid? ImportId { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? DeletedAt { get; set; }
}