
using Common.Dtos.Profile;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class ProfileService(HttpClient _httpClient) : IProfileService
{

    public async Task<IEnumerable<ProfileDto>> GetProfiles()
    {
        var response = await _httpClient.GetAsync("api/Profile");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProfileDto>>();
    }

    public async Task<ProfileDto> GetProfile(Guid profileId)
    {
        var response = await _httpClient.GetAsync($"api/Profile/{profileId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProfileDto>();
    }

    public async Task<ProfileDto> CreateProfile(ProfileDto profileDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Profile", profileDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProfileDto>();
    }

    public async Task<ProfileDto> UpdateProfile(Guid profileId, ProfileDto updatedProfileDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Profile/{profileId}", updatedProfileDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProfileDto>();
    }

    public async Task DeleteProfile(Guid profileId)
    {
        var response = await _httpClient.DeleteAsync($"api/Profile/{profileId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<SkillDto>> GetSkills()
    {
        var response = await _httpClient.GetAsync("api/Profile/skills");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<SkillDto>>();
    }

    public async Task<IEnumerable<SkillDto>> GetSkillsForProfile(Guid profileId)
    {
        var response = await _httpClient.GetAsync($"api/Profile/{profileId}/skills");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<SkillDto>>();
    }

    public async Task AddSkillForProfile(Guid profileId, Guid skillId)
    {
        var response = await _httpClient.PostAsync($"api/Profile/{profileId}/skills/{skillId}", null);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task DeleteSkillForProfile(Guid profileId, Guid skillId)
    {
        var response = await _httpClient.DeleteAsync($"api/Profile/{profileId}/skills/{skillId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<ExperienceDto>> GetExperiencesForProfile(Guid profileId)
    {
        var response = await _httpClient.GetAsync($"api/Profile/{profileId}/experiences");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ExperienceDto>>();
    }

    public async Task<ExperienceDto> CreateExperience(Guid profileId, ExperienceDto experienceDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Profile/{profileId}/experiences", experienceDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExperienceDto>();
    }

    public async Task<ExperienceDto> UpdateExperience(Guid profileId, ExperienceDto updatedExperienceDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Profile/{profileId}/experiences/{updatedExperienceDto.ExperienceId}", updatedExperienceDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExperienceDto>();
    }

    public async Task DeleteExperience(Guid profileId, Guid experienceId)
    {
        var response = await _httpClient.DeleteAsync($"api/Profile/{profileId}/experiences/{experienceId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<EducationDto>> GetEducationsForProfile(Guid profileId)
    {
        var response = await _httpClient.GetAsync($"api/Profile/{profileId}/educations");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<EducationDto>>();
    }

    public async Task<EducationDto> CreateEducation(Guid profileId, EducationDto educationDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Profile/{profileId}/educations", educationDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EducationDto>();
    }

    public async Task<EducationDto> UpdateEducation(Guid profileId, EducationDto updatedEducationDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Profile/{profileId}/educations/{updatedEducationDto.EducationId}", updatedEducationDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EducationDto>();
    }

    public async Task DeleteEducation(Guid profileId, Guid educationId)
    {
        var response = await _httpClient.DeleteAsync($"api/Profile/{profileId}/educations/{educationId}");
        response.EnsureSuccessStatusCode();
    }
    public async Task<ProfileDto> GetProfiles()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Profile");
                
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new ProfileDto();
                }
                return await response.Content.ReadFromJsonAsync<ProfileDto>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching profile: {ex.Message}", ex);
        }
    }
}