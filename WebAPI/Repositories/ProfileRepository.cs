using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
    
    public async Task<IEnumerable<Profile>> GetProfilesAsync()
    {
        return await _dbContext.Profiles.OrderBy(p => p.ProfileId).ToListAsync();
    }

    public async Task<Profile> GetProfileAsync(Guid profileId)
    {
        return await _dbContext.Profiles.FirstOrDefaultAsync(p => p.ProfileId == profileId);
    }

    public async Task<bool> ProfileExistsAsync(Guid profileId)
    {
        return await _dbContext.Profiles.AnyAsync(p => p.ProfileId == profileId);
    }

    public async Task<int> UpdateProfileAsync(Profile profile)
    {
        _dbContext.Profiles.Update(profile);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task AddProfileAsync(Profile profile)
    {
        await _dbContext.Profiles.AddAsync(profile);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProfileAsync(Guid profileId)
    {
        var profile = await _dbContext.Profiles.FindAsync(profileId);
        if (profile != null)
        {
            _dbContext.Profiles.Remove(profile);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<List<Profile>> GetProfiles()
    {
        var db = new ApplicationDbContext();
        
        var profiles = await db.Profiles
            .Include(p => p.ProfileSkills)
            .ThenInclude(ps => ps.Skill)
            .Include(p => p.Experiences)
            .Include(p => p.Educations)
            .ToListAsync();

        return profiles;
    }
}
