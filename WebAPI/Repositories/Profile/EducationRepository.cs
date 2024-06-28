using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;
public class EducationRepository : IEducationRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
    
    public async Task<ICollection<Education>> GetEducationsAsync()
    {
        return await _dbContext.Educations.OrderBy(p => p.EducationId).ToListAsync();
    }

    public async Task<Education> GetEducationAsync(Guid id)
    {
        return await _dbContext.Educations.FirstOrDefaultAsync(e => e.EducationId == id);
    }

    public async Task<bool> EducationExistsAsync(Guid educationId)
    {
        return await _dbContext.Educations.AnyAsync(e => e.EducationId == educationId);
    }

    public async Task<int> UpdateEducationAsync(Education education)
    {
        _dbContext.Educations.Update(education);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Education>> GetProfileEducationsAsync(Guid profileId)
    {
        return await _dbContext.Educations.Where(e => e.Profile.ProfileId == profileId).ToListAsync();
    }

    public async Task DeleteProfileEducationAsync(Guid educationId)
    {
        var education = await _dbContext.Educations.FindAsync(educationId);

        if (education != null)
        {
            _dbContext.Educations.Remove(education);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddEducationAsync(Education education)
    {
        await _dbContext.Educations.AddAsync(education);
        await _dbContext.SaveChangesAsync();
    }
}