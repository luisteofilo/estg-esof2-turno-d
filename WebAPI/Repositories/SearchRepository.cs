using Common.Dtos.Job;
using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    
    public async Task<IEnumerable<Profile>> GetSearchResultsAsync(string firstName = null, string skill = null, string location = null)
    {
        IQueryable<Profile> query = _dbContext.Profiles;

        if (!string.IsNullOrEmpty(firstName))
        {
            query = query.Where(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
        }

        if (!string.IsNullOrEmpty(skill))
        {
            query = query.Where(p => p.ProfileSkills.Any(ps => ps.Skill.Name.ToLower().Contains(skill.ToLower())));
        }

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(p => p.Location.ToLower().Contains(location.ToLower()));
        }

        return await query.ToListAsync();
    }

    public async Task<bool> ProfileExistsAsync(string firstName)
    {
        return await _dbContext.Profiles.AnyAsync(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
    }
    
    public async Task<IEnumerable<string>> GetLocationsAsync()
    {
        return await _dbContext.Profiles.Select(p => p.Location).Distinct().ToListAsync();
    }
    
    /*
    public async Task<IEnumerable<Profile>> GetSearchResultsAsync(string firstName)
    {
        return await _dbContext.Profiles.Where(p => p.FirstName.ToLower().Contains(firstName.ToLower())).ToListAsync();
    }
    
    public async Task<bool> ProfileExistsAsync(string firstName)
    {
        return await _dbContext.Profiles.AnyAsync(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
    }
    
    public async Task<IEnumerable<Profile>> GetSearchResultsSkillsAsync(string firstName, string skill)
    {
        return await _dbContext.Profiles.Where(p => p.FirstName.Contains(firstName) && p.ProfileSkills.Any(ps => ps.Skill.Name.Contains(skill))).ToListAsync();
    }
    
    public async Task<IEnumerable<Profile>> GetSearchResultsLocationAsync(string firstName, string location)
    {
        return await _dbContext.Profiles.Where(p => p.FirstName.Contains(firstName) && p.Location.Contains(location)).ToListAsync();
    }
    
    public async Task<IEnumerable<Profile>> GetSearchResultsSkillsLocationAsync(string firstName, string skill, string location)
    {
        return await _dbContext.Profiles
            .Where(p => p.FirstName.Contains(firstName) 
                        && p.ProfileSkills.Any(ps => ps.Skill.Name.Contains(skill)) 
                        && p.Location.Contains(location)) 
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Profile>> GetResultsSkillsAsync(string skill)
    {
        return await _dbContext.Profiles.Where(p =>p.ProfileSkills.Any(ps => ps.Skill.Name.Contains(skill))).ToListAsync();
    }
    
    public async Task<IEnumerable<Profile>> GetResultsLocationAsync(string location)
    {
        return await _dbContext.Profiles.Where(p => p.Location.Contains(location)).ToListAsync();
    }
    
    public async Task<IEnumerable<Profile>> GetResultsLocationSkillAsync(string location, string skill)
    {
        return await _dbContext.Profiles.Where(p => p.Location.Contains(location) && p.ProfileSkills.Any(ps => ps.Skill.Name.Contains(skill))).ToListAsync();
    }
    
    public async Task<IEnumerable<string>> GetLocationsAsync()
    {
        return await _dbContext.Profiles.Select(p => p.Location).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetJobBySkillAsync(string skill)
    {
       
        var skillId = await _dbContext.Skills
            .Where(s => s.Name == skill)
            .Select(s => s.SkillId)
            .FirstOrDefaultAsync();

        if (skillId == Guid.Empty)
        {
            return Enumerable.Empty<Job>();
        }

        
        var jobIds = await _dbContext.JobSkills
            .Where(js => js.SkillId == skillId && js.IsRequired == true)
            .Select(js => js.JobId)
            .Distinct()
            .ToListAsync();

        
        var jobs = await _dbContext.Jobs.Where(j => jobIds.Contains(j.JobId)).ToListAsync();

        return jobs;
    }

    public async Task<IEnumerable<Job>> GetJobByLocationAsync(string location)
    {
        return await _dbContext.Jobs.Where(j => j.Localization.Contains(location)).ToListAsync();
    }
    
    public async Task<IEnumerable<Job>> GetJobByPositionAsync(string position)
    {
        return await _dbContext.Jobs.Where(j => j.Position.Contains(position)).ToListAsync();
    }*/
}