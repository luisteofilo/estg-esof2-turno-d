using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface ISearchService
{
    bool searchPerformed { get; set; }
    Task<IEnumerable<ProfileDto>> GetResults(string firstName);

    Task<IEnumerable<ProfileDto>> GetResultsBySkill(string skill);

    Task<IEnumerable<ProfileDto>> GetResultsBySkill_Name(string skill, string firstName);
    
    Task<IEnumerable<ProfileDto>> GetResultsBySkill_Name_Location(string skill, string firstName, string location);

    Task<IEnumerable<string>> GetLocations();

    Task<IEnumerable<ProfileDto>> GetResultByLocation(string location);

    Task<IEnumerable<ProfileDto>> GetResultsByLocation_Name(string location, string firstName);

    Task<IEnumerable<ProfileDto>> GetResultsByLocation_Skill(string location, string skill);
}