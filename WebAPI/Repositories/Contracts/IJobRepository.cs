namespace ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.DBLayer.Entities;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetJobsAsync();
    Task<Job> GetJobByIdAsync(Guid jobId);
    Task AddJobAsync(Job job);
    //Task UpdateJobAsync(Job job);
    //Task DeleteJobAsync(Guid jobId);
}