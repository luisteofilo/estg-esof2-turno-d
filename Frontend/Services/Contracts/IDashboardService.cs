using Common.Dtos.Job;

namespace Frontend.Services.Contracts;
using Common.Dtos.Profile;

public interface IDashboardService
{
    Task<IEnumerable<ProfileSkillDto>> GetProfileSkills();
    
    Task<IEnumerable<DashboardJobDTo>> GetJobSkills();
    
    Task<IEnumerable<ExperienceDto>> GetExperiences();
    
}