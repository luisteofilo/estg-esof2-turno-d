namespace Frontend.Services.Contracts;
using Common.Dtos.Profile;

public interface IDashboardService
{
    Task<IEnumerable<ProfileSkillDto>> GetProfileSkills();
    Task<IEnumerable<string>> GetProfileListOfSkills();
}