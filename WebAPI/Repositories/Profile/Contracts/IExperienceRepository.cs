using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IExperienceRepository
{
    Task<ICollection<Experience>> GetExperiencesAsync();
    Task<Experience> GetExperienceAsync(Guid id);
    Task<bool> ExperienceExistsAsync(Guid experienceId);
    Task<int> UpdateExperienceAsync(Experience experience);
    Task<ICollection<Experience>> GetProfileExperiencesAsync(Guid profileId);
    Task AddExperienceAsync(Experience experience);
    Task DeleteExperienceAsync(Guid experienceId);
}