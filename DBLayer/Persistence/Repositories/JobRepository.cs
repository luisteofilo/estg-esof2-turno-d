using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;

namespace ESOF.WebApp.DBLayer.Persistence.Repositories;

public class JobRepository(ApplicationDbContext _dbContext) : BaseRepository(_dbContext), IJobRepository
{
    public async Task Create(Job job)
    {
        await _dbContext.Jobs.AddAsync(job);
    }
}