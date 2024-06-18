
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Persistence.Interfaces;

public interface IJobRepository
{
    Task Create(Job job, CancellationToken cancellationToken);

    Task<Job> GetJobByUrl(string url, CancellationToken cancellationToken);

    Task Update(Job? job, CancellationToken cancellationToken);
}