using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ISkillRepository
{
    // CRUD for skills (without profiles)
    Task<ICollection<Skill>> GetSkillsAsync();
    Task<bool> SkillExistsAsync(Guid skillId);
    Task<int> UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(Guid skillId);
    Task<Skill> CreateSkillAsync(Skill skill);
    Task<Skill> GetSkillByIdAsync(Guid skillId);
    
    // CRUD for skills (with profiles)
    Task AddSkillToProfileAsync(Guid skillId, Guid profileId);
    Task RemoveSkillFromProfileAsync(Guid skillId, Guid profileId);
    Task<ICollection<Skill>> GetProfileSkillsAsync(Guid profileId);
}