using Common.Dtos.Job;
using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface IJobService
{
    Task<JobDto> CreateJob(Guid JobId, Guid ClientId, JobDto jobDto);
    
    //Task<IEnumerable<SkillDto>> GetSkills();
}