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
    
    public async Task<IEnumerable<Job>> GetJobsAsync(string company = null, string location = null, string experience = null)
    {
        var query = _dbContext.Jobs.AsQueryable();

        if (!string.IsNullOrEmpty(company))
        {
            query = query.Where(j => j.Company.ToLower() == company.ToLower());
        }

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(j => j.Localization.ToLower() == location.ToLower());
        }

        if (!string.IsNullOrEmpty(experience))
        {
            query = query.Where(j => j.Experience.ToLower() == experience.ToLower());
        }

        return await query.ToListAsync();
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
    
    public async Task<IEnumerable<string>> GetAllJobsCompaniesAsync()
    {
        return await _dbContext.Jobs
                               .Where(j => j.Company != null)
                               .Select(j => j.Company)
                               .Distinct()
                               .ToListAsync();
    }
    
    public async Task<IEnumerable<string>> GetAllJobsLocationsAsync()
    {
        return await _dbContext.Jobs
            .Where(j => j.Localization != null)
            .Select(j => j.Localization)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<string>> GetAllJobsExperienceAsync()
    {
        return await _dbContext.Jobs
            .Where(j => j.Experience!= null)
            .Select(j => j.Experience)
            .Distinct()
            .ToListAsync();
    }
}