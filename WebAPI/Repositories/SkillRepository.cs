using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    // CRUD for skills (without profiles)
    
    public async Task<ICollection<Skill>> GetSkillsAsync()
    {
        return await _dbContext.Skills.OrderBy(p => p.SkillId).ToListAsync();
    }

    public async Task<bool> SkillExistsAsync(Guid skillId)
    {
        return await _dbContext.Skills.AnyAsync(p => p.SkillId == skillId);
    }

    public async Task<int> UpdateSkillAsync(Skill skill)
    {
        _dbContext.Skills.Update(skill);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSkillAsync(Guid skillId)
    {
        var skill = await _dbContext.Skills.FindAsync(skillId);

        if (skill != null)
        {
            _dbContext.Skills.Remove(skill);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    public async Task<Skill> CreateSkillAsync(Skill skill)
    {
        var entityEntry = await _dbContext.Skills.AddAsync(skill);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
    

    public async Task<Skill> GetSkillByIdAsync(Guid skillId)
    {
        return await _dbContext.Skills.FindAsync(skillId);
    }
    
    // CRUD for skills (with profiles)

    public async Task AddSkillToProfileAsync(Guid skillId, Guid profileId)
    {
        var profileSkill = new ProfileSkill
        {
            SkillId = skillId,
            ProfileId = profileId
        };

        await _dbContext.ProfileSkills.AddAsync(profileSkill);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveSkillFromProfileAsync(Guid skillId, Guid profileId)
    {
        var profileSkill = await _dbContext.ProfileSkills
            .FirstOrDefaultAsync(ps => ps.SkillId == skillId && ps.ProfileId == profileId);

        if (profileSkill != null)
        {
            _dbContext.ProfileSkills.Remove(profileSkill);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Skill>> GetProfileSkillsAsync(Guid profileId)
    {
        return await _dbContext.ProfileSkills
            .Where(ps => ps.ProfileId == profileId)
            .Select(ps => ps.Skill)
            .ToListAsync();
    }
}