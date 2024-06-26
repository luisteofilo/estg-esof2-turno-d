using Common.Dtos.Job;
using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface IJobService
{
    Task<JobDto> CreateJob(Guid ClientId, JobDto jobDto);

    Task<JobSkillDto> CreateJobSkill(Guid JobId, Guid SkillId, bool isRequired, JobSkillDto jobSkillDto);
    
    Task<ICollection<JobDto>> GetJobs();

}