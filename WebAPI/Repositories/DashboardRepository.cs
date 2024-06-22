using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
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

        public async Task<IEnumerable<Skill>> GetListOfSkillsAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }
    }
