using Common.Dtos.Job;

namespace Frontend.Services.Contracts;

public interface IJobService
{
    Task<JobDto> CreateJob(Guid ClientId, JobDto jobDto);
    
    Task<IEnumerable<JobDto>> GetJobsAsync();

    Task<JobSkillDto> CreateJobSkill(Guid JobId, Guid SkillId, bool isRequired, JobSkillDto jobSkillDto);

    Task<IEnumerable<JobDto>> GetJobs();
}