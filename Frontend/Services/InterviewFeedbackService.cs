using System.Net.Http.Json;
using Common.Dtos.Interview;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities.Interviews;
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
        
        public async Task<IEnumerable<CandidateDto>> GetCandidates()
        {
            var response = await _httpClient.GetAsync("api/InterviewFeedback/Candidates-from-feedback");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<CandidateDto>>();
        }
        
        public async Task<IEnumerable<InterviewDto>> GetInterviews()
        {
            var response = await _httpClient.GetAsync("api/InterviewFeedback/Interviews-from-feedback");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<InterviewDto>>();
        }
        
        public async Task<IEnumerable<InterviewerDto>> GetInterviewers()
        {
            var response = await _httpClient.GetAsync("api/InterviewFeedback/Interviewers-from-feedback");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<InterviewerDto>>();
        }


   

        public async Task<JobDto?> UpdateJob(Guid JobId, JobDto jobDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/InterviewFeedback/job/{JobId}", jobDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobDto>();
        }
    }
}
