using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IEducationRepository
{
    Task<ICollection<Education>> GetEducationsAsync();
    Task<Education> GetEducationAsync(Guid id);
    Task<bool> EducationExistsAsync(Guid educationId);
    Task<int> UpdateEducationAsync(Education education);
    Task<ICollection<Education>> GetProfileEducationsAsync(Guid profileId);
    Task DeleteProfileEducationAsync(Guid educationId);
    Task AddEducationAsync(Education education);
}