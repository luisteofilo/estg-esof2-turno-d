using System.Collections.ObjectModel;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities;
using Frontend.Services.Contracts;
using System.Net.Http.Json;

namespace Frontend.Services;

public class InterviewFeedbackService : IinterviewFeedback
{
    private readonly HttpClient _httpClient;

    public InterviewFeedbackService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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

    public async Task<InterviewFeedbackDTO?> InterviewFeedbackExistsAsync(Guid interviewFeedbackId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/{interviewFeedbackId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> UpdateInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedback, Guid interviewFeedbackId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/{interviewFeedbackId}", interviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task DeleteInterviewFeedbackAsync(Guid interviewFeedbackId)
    {
        var response = await _httpClient.DeleteAsync($"api/InterviewFeedback/{interviewFeedbackId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<InterviewFeedbackDTO> AddInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedback)
    {
        var response = await _httpClient.PostAsJsonAsync("api/InterviewFeedback", interviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByJob(Guid jobId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/job/{jobId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByCandidate(Guid candidateId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/candidate/{candidateId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterview(Guid interviewId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/interview/{interviewId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterviewer(Guid interviewerId)
    {
        var response = await _httpClient.GetAsync($"api/InterviewFeedback/interviewer/{interviewerId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
    }

    public async Task<JobDto?> UpdateJob(Guid jobId, JobDto jobDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/update-job/{jobId}", jobDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<JobDto>();
    }
}
