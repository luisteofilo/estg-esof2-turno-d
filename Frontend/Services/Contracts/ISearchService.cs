using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface ISearchService
{
    bool searchPerformed { get; set; }
    Task<IEnumerable<ProfileDto>> GetResults(string firstName);

    Task<IEnumerable<ProfileDto>> GetResultsBySkill(string skill);

    Task<IEnumerable<ProfileDto>> GetResultsBySkill_Name(string skill, string firstName);
}