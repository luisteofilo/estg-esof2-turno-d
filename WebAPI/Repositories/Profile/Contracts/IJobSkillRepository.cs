using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IJobSkillRepository
{
    Task<IEnumerable<JobSkill>> GetJobSkillsAsync();
    Task<JobSkill> GetJobSkillsByIdAsync(Guid jobId, Guid skillId);
    Task AddJobSkillAsync(JobSkill jobSkill);
}