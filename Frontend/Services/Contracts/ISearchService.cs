using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface ISearchService
{
    void SetName(string name);
    Task<ProfileDto> GetResults(string firstName);
}