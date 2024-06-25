using System.Collections.ObjectModel;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class InterviewFeedbackService(HttpClient _httpClient) : IinterviewFeedback

{

    public async Task<ICollection<InterviewFeedbackDTO>> GetInterviewsFeedback()
    {
        var response = await _httpClient.GetAsync("api/InterviewFeedback");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Collection<InterviewFeedbackDTO>>();
    }   
    
    public async Task<InterviewFeedbackDTO> GetInterviewFeedback(Guid interviewFeedbackId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interviewFeedbackId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{InterviewFeedbackId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> UpdateInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedback,
        Guid InterviewFeedbackId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/{InterviewFeedbackId}", interviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId)
    {
        var response = await _httpClient.DeleteAsync($"api/InterviewFeedback/{InterviewFeedbackId}");
        response.EnsureSuccessStatusCode();
    }
    public async Task<InterviewFeedbackDTO> AddInterviewFeedbackAsync(InterviewFeedbackDTO InterviewFeedback)
    {
        var response = await _httpClient.PostAsJsonAsync("api/InterviewFeedback", InterviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }
    
    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByJob(Guid JobId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{JobId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }
    
    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByCandidate(Guid candidate)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{candidate}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }
    
    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterview(Guid interview)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interview}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }
    
    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterviewer(Guid interviewer)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interviewer}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<JobDto?> UpdateJob(Guid JobId, JobDto jobDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/{JobId}", jobDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobDto>();
    }
    
    
}