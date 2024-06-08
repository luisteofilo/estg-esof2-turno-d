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
            .FirstOrDefaultAsync(p => p.ProfileId == Guid.Parse("d841ca87-e89d-4eea-9953-95f2d44dcbda"));

        return profile;
    }
}