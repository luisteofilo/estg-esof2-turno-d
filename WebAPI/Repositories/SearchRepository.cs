using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class SearchRepository : ISearchRepository
{
    private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

    public async Task<IEnumerable<Profile>> GetSearchResultsAsync(string firstName)
    {
        return await _dbContext.Profiles.Where(p => p.FirstName.Contains(firstName)).ToListAsync();
    }
    
    public async Task<bool> ProfileExistsAsync(string firstName)
    {
        return await _dbContext.Profiles.AnyAsync(p => p.FirstName.Contains(firstName));
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
}