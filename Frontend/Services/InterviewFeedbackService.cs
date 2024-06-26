using System.Net.Http.Json;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using Frontend.Services.Contracts;

namespace Frontend.Services
{
    public class InterviewFeedbackService : IInterviewFeedbackService
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
            return await response.Content.ReadFromJsonAsync<ICollection<InterviewFeedbackDTO>>();
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

        public async Task<InterviewFeedbackDTO?> UpdateInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedback, Guid InterviewFeedbackId)
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

        public async Task<InterviewFeedbackDTO?> AddInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedback)
        {
            var response = await _httpClient.PostAsJsonAsync("api/InterviewFeedback", interviewFeedback);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
        }

        public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByJob(Guid JobId)
        {
            var response = await _httpClient.GetAsync($"api/InterviewFeedback/job/{JobId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
        }

        public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByCandidate(Guid candidate)
        {
            var response = await _httpClient.GetAsync($"api/InterviewFeedback/candidate/{candidate}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
        }

        public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterview(Guid interview)
        {
            var response = await _httpClient.GetAsync($"api/InterviewFeedback/interview/{interview}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
        }

        public async Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterviewer(Guid interviewer)
        {
            var response = await _httpClient.GetAsync($"api/InterviewFeedback/interviewer/{interviewer}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InterviewFeedbackDTO>();
        }

        public async Task<JobDto?> UpdateJob(Guid JobId, JobDto jobDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/job/{JobId}", jobDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobDto>();
        }
    }
}
