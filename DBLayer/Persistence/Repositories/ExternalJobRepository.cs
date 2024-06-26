using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Persistence.Repositories;

public class ExternalJobRepository(ApplicationDbContext _dbContext) : BaseRepository(_dbContext), IExternalJobRepository
{
    public async Task Create(Job job, CancellationToken cancellationToken)
    {
        await _dbContext.Jobs.AddAsync(job, cancellationToken);
    }

    public async Task<Job?> GetJobByUrl(string url, CancellationToken cancellationToken)
    {
        return await _dbContext.Jobs.Include(jobs => jobs.Import).FirstOrDefaultAsync(job => job.Import!.Url == url, cancellationToken);
    }

    public async Task Update(Job job, CancellationToken cancellationToken)
    {
        await _dbContext.Jobs
         .Where(j => j.JobId == job.JobId)
         .ExecuteUpdateAsync(setters => setters
             .SetProperty(j => j.Position, job.Position)
             .SetProperty(j => j.Localization, job.Localization)
             .SetProperty(j => j.Description, job.Description)
             .SetProperty(j => j.Company, job.Company)
             .SetProperty(j => j.OtherDetails, job.OtherDetails)
             .SetProperty(j => j.UpdatedAt, job.UpdatedAt), cancellationToken);
    }
}