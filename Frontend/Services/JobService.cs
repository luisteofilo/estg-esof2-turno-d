﻿using Common.Dtos.Job;
using Common.Dtos.Profile;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class JobService(HttpClient _httpClient) : IJobService
{
    public async Task<JobDto> CreateJob(Guid JobId, Guid ClientId, JobDto jobDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Job", jobDto);
        /*
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobDto>();
        */
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<JobDto>();
        }
        else
        {
            // Log the error message
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error creating job: {errorMessage}");
        }
    }
    /*
    public async Task<List<SkillDto>> GetSkills()
    {
        var response = await _httpClient.GetAsync("api/Skill");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<SkillDto>>();
    }
    */
}