using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ISearchRepository
{
    Task<IEnumerable<Profile>> GetSearchResultsAsync(string firstName);

    Task<bool> ProfileExistsAsync(string firstName);
    
    Task<IEnumerable<Profile>> GetSearchResultsSkillsAsync(string firstName, string skill);

    Task<IEnumerable<Profile>> GetSearchResultsLocationAsync(string firstName, string location);

    Task<IEnumerable<Profile>> GetSearchResultsSkillsLocationAsync(string firstName, string skill, string location);

    Task<IEnumerable<Profile>> GetResultsSkillsAsync(string skill);

    Task<IEnumerable<Profile>> GetResultsLocationAsync(string location);

    Task<IEnumerable<Profile>> GetResultsLocationSkillAsync(string location, string skill);

    Task<IEnumerable<string>> GetLocationsAsync();

    Task<IEnumerable<Job>> GetJobBySkillAsync(string skill);

    Task<IEnumerable<Job>> GetJobByLocationAsync(string location);

    Task<IEnumerable<Job>> GetJobByPositionAsync(string position);
}