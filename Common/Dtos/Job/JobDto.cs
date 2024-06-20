using ESOF.WebApp.DBLayer.Entities;
using Common.Dtos.Profile;

namespace Common.Dtos.Job;

public class JobDto
{
    public Guid JobId { get; set; }
    
    public Guid ClientId { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public String Position { get; set; }
    
    public CommitmentType? Commitment { get; set; }
    
    public RemoteType? Remote { get; set; }
    
    public String Localization { get; set; }
    
    public EducationLevel? Education { get; set; }
    
    public String Experience { get; set; }
    
    public String? Description { get; set; }
    
    public List<SkillDto> RequiredSkills { get; set; } = new List<SkillDto>();
    public List<SkillDto> NiceToHaveSkills { get; set; } = new List<SkillDto>();
    
}