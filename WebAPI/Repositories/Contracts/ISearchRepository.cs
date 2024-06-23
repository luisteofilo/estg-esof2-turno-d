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

    Task<IEnumerable<Profile>> GetResultsSkillsAsync(string skill);

    Task<IEnumerable<Profile>> GetResultsLocationAsync(string location);
}