using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;
    

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    public async Task<IEnumerable<Job>> GetJobsAsync()
    { 
        return await _dbContext.Jobs.ToListAsync();
    }
    
    
    public async Task<IEnumerable<Client>> GetClients()
    { 
        return await _dbContext.Clients.ToListAsync();
    }


    public async Task<Job> GetJobByIdAsync(Guid jobId)
    { 
        return await _dbContext.Jobs.FindAsync(jobId);
    }

    public async Task AddJobAsync(Job job)
    { 
        await _dbContext.Jobs.AddAsync(job); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateJobAsync(Job job)
    { 
        job.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Jobs.Update(job);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteJobAsync(Guid jobId)
    {
        var job = await _dbContext.Jobs.FindAsync(jobId);
        if (job != null)
        {
            job.DeletedAt = DateTimeOffset.UtcNow;
            _dbContext.Jobs.Update(job);
            await _dbContext.SaveChangesAsync();
        }
    }
}