using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class JobSkillRepository : IJobSkillRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
    
    public async Task<IEnumerable<JobSkill>> GetJobSkillsAsync()
    { 
        return await _dbContext.JobSkills.ToListAsync();
    }
    
    public async Task<JobSkill> GetJobSkillsByIdAsync(Guid jobId, Guid skillId)
    { 
        return await _dbContext.JobSkills.FindAsync(jobId, skillId);
    }
    
    public async Task AddJobSkillAsync(JobSkill jobSkill)
    { 
        await _dbContext.JobSkills.AddAsync(jobSkill); 
        await _dbContext.SaveChangesAsync();
    }
    
}