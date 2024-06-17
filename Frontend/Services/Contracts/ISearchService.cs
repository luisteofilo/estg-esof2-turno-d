using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface ISearchService
{
    Task<ProfileDto> GetResults();
}