using Common.Dtos.Job;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Context;
using Common.Dtos.Profile;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    public async Task<IEnumerable<ProfileSkillDto>> GetProfileSkillsAsync()
    {
        var profileSkills = await _dbContext.ProfileSkills
            .Include(ps => ps.Skill)
            .Select(ps => new ProfileSkillDto
            {
                ProfileId = ps.ProfileId,
                SkillId = ps.SkillId,
                SkillName = ps.Skill.Name  
            })
            .OrderBy(p => p.SkillId)
            .ToListAsync();
    
        return profileSkills;
    }
        
    public async Task<IEnumerable<DashboardJobDTo>> GetJobSkillsAsync()
    {
        var jobSkills = await _dbContext.JobSkills
            .Include(js => js.Skill)
            .Select(js => new DashboardJobDTo()
            {
                JobId = js.JobId,
                SkillId = js.SkillId,
                SkillName = js.Skill.Name  
            })
            .OrderBy(p => p.SkillId)
            .ToListAsync();
    
        return jobSkills;
    }
        
    public async Task<IEnumerable<ExperienceDto>> GetExperiencesAsync()
    {
        var experiences = await _dbContext.Experiences
            .Select(e => new ExperienceDto
            {
                ExperienceId = e.ExperienceId,
                Name = e.Name
            })
            .OrderBy(p => p.ExperienceId)
            .ToListAsync();

        return experiences;
    }


}