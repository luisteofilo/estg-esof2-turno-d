namespace ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.DBLayer.Entities;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetJobsAsync();
    Task<IEnumerable<Job>> GetJobsAsync(string company = null, string location = null, string experience = null);
    Task<Job> GetJobByIdAsync(Guid jobId);
    Task AddJobAsync(Job job);
    Task UpdateJobAsync(Job job);
    Task DeleteJobAsync(Guid jobId);
    Task<IEnumerable<string>>  GetAllJobsCompaniesAsync();
    Task<IEnumerable<string>> GetAllJobsExperienceAsync();
    Task<IEnumerable<string>> GetAllJobsLocationsAsync();
}