using Common.Dtos.Job;

namespace Frontend.Services.Contracts;
using Common.Dtos.Profile;

public interface IDashboardService
{
    Task<IEnumerable<DashboardProfilesSkillDTO>> GetProfileSkills();
    
    Task<IEnumerable<DashboardJobDTO>> GetJobSkills();
    
    Task<IEnumerable<ExperienceDto>> GetExperiences();
    
}