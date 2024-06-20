using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;
    
public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;
    
    public JobRepository()
    { 
        _context = new ApplicationDbContext();
    }

    
    public JobRepository(ApplicationDbContext context)
    { 
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetJobsAsync()
    { 
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job> GetJobByIdAsync(Guid jobId)
    { 
        return await _context.Jobs.FindAsync(jobId);
    }

    public async Task AddJobAsync(Job job)
    { 
        await _context.Jobs.AddAsync(job); 
        await _context.SaveChangesAsync();
    }

    public async Task UpdateJobAsync(Job job)
    { 
        _context.Jobs.Update(job); 
        await _context.SaveChangesAsync();
    }

    public async Task DeleteJobAsync(Guid jobId)
    {
        var job = await _context.Jobs.FindAsync(jobId);
        if (job != null)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }
}