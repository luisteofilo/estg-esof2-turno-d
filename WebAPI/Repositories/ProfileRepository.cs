using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class ProfileRepository() : IProfileRepository
{
    public async Task<Profile> GetProfile()
    {
        var db = new ApplicationDbContext();
        
        var profile = await db.Profiles
            .Include(p => p.ProfileSkills)
            .ThenInclude(ps => ps.Skill)
            .Include(p => p.Experiences)
            .Include(p => p.Educations)
            // Hard coded Profile ID
            .FirstOrDefaultAsync(p => p.ProfileId == Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0155f"));

        return profile;
    }
}