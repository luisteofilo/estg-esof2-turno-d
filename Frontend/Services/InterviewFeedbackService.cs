using System.Collections.ObjectModel;
using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class InterviewFeedbackService(HttpClient _httpClient) : IinterviewFeedback
{
    public async Task<ICollection<InterviewFeedback>> GetInterviewsFeedback()
    {
        var response = await _httpClient.GetAsync("api/InterviewFeedback");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Collection<InterviewFeedback>>();
    }   
    
    public async Task<InterviewFeedback> GetInterviewFeedback(Guid interviewFeedbackId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interviewFeedbackId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }

    public async Task<InterviewFeedback?> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{InterviewFeedbackId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }

    public async Task<InterviewFeedback?> UpdateInterviewFeedbackAsync(InterviewFeedback interviewFeedback,
        Guid InterviewFeedbackId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/{InterviewFeedbackId}", interviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }

    public async Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId)
    {
        var response = await _httpClient.DeleteAsync($"api/InterviewFeedback/{InterviewFeedbackId}");
        response.EnsureSuccessStatusCode();
    }
    public async Task<InterviewFeedback?> AddInterviewFeedbackAsync(InterviewFeedback InterviewFeedback)
    {
        var response = await _httpClient.PostAsJsonAsync("api/InterviewFeedback", InterviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }
    
    public async Task<InterviewFeedback?> GetInterviewFeedbackByJob(Guid JobId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{JobId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }
    
    public async Task<InterviewFeedback?> GetInterviewFeedbackByCandidate(Guid candidate)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{candidate}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }
    
    public async Task<InterviewFeedback?> GetInterviewFeedbackByInterview(Guid interview)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interview}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }
    
    public async Task<InterviewFeedback?> GetInterviewFeedbackByInterviewer(Guid interviewer)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interviewer}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedback>();
    }
    
}