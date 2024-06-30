using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Import
{
    [Key]
    public Guid ImportId { get; set; } = Guid.NewGuid();

    [Required]
    public string Url { get; set; }
    public ICollection<Job> Jobs { get; set; } = new List<Job>();

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? DeletedAt { get; set; }

    public Job AddJob(Guid clientId, ICollection<Position>? positions, string location, string content, string company, string otherDetails)
    {
        var job = new Job
        {
            ClientId = clientId,
            Positions = positions,
            Localization = location,
            Description = content,
            Company = company,
            OtherDetails = otherDetails,
            ImportId = this.ImportId,
        };

        Jobs.Add(job);

        return job;
    }
}