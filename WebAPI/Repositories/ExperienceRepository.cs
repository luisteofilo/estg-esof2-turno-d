using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    public async Task<ICollection<Experience>> GetExperiencesAsync()
    {
        return await _dbContext.Experiences.OrderBy(p => p.ExperienceId).ToListAsync();
    }

    public async Task<Experience> GetExperienceAsync(Guid id)
    {
        return await _dbContext.Experiences.FirstOrDefaultAsync(e => e.ExperienceId == id);
    }

    public async Task<bool> ExperienceExistsAsync(Guid experienceId)
    {
        return await _dbContext.Experiences.AnyAsync(e => e.ExperienceId == experienceId);
    }

    public async Task<int> UpdateExperienceAsync(Experience experience)
    {
        _dbContext.Experiences.Update(experience);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Experience>> GetProfileExperiencesAsync(Guid profileId)
    {
        return await _dbContext.Experiences.Where(e => e.ProfileId == profileId).ToListAsync();
    }

    public async Task AddExperienceAsync(Experience experience)
    {
        await _dbContext.Experiences.AddAsync(experience);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteExperienceAsync(Guid experienceId)
    {
        var experience = await _dbContext.Experiences.FindAsync(experienceId);

        if (experience != null)
        {
            _dbContext.Experiences.Remove(experience);
            await _dbContext.SaveChangesAsync();
        }
    }
}