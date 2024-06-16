using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Import
{
    [Key]
    public Guid ImportId { get; set; }

    [Required]
    public string Url { get; set; }
    public ICollection<Job> Jobs { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? DeletedAt { get; set; }
}