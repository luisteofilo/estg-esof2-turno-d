using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Job;

public class JobDto
{
    public Guid JobId { get; set; }
    
    //public Guid ClientId { get; set; }
    
    //public Client Client { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public String Position { get; set; }
    
    public CommitmentType? Commitment { get; set; }
    
    public RemoteType? Remote { get; set; }
    
    public String Localization { get; set; }
    
    public EducationLevel? Education { get; set; }
    
    public ICollection<JobSkill> RequiredSkills { get; set; }
    
    public ICollection<JobSkill> NiceToHaveSkills { get; set; }
    
    public String Experience { get; set; }
    
    public String Language { get; set; }
    
    public String? Description { get; set; }
    
}