using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface ISearchService
{
    void SetName(string name);
    Task<IEnumerable<ProfileDto>> GetResults(string firstName);

    Task<IEnumerable<ProfileDto>> GetResultsBySkill(string skill);
}