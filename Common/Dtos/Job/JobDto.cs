using ESOF.WebApp.DBLayer.Entities;
using Helpers.Job;

namespace Common.Dtos.Job;

public class JobDto
{
    public Guid JobId { get; set; }
    
    public Guid ClientId { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public ICollection<ESOF.WebApp.DBLayer.Entities.Position>? Positions { get; set; }
    
    public CommitmentType? Commitment { get; set; }
    
    public RemoteType? Remote { get; set; }

    public string Localization { get; set; }

    public EducationLevel? Education { get; set; }

    public string? Company { get; set; }

    public string? OtherDetails { get; set; }

    public string? Experience { get; set; }

    public string? Description { get; set; }

    public Import? Import { get; set; }
}