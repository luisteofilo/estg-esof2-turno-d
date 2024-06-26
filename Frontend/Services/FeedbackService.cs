using Common.Dtos.Feedback;
using Frontend.Services.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public class FeedbackService(HttpClient httpClient) : IFeedbackService
    {
        public async Task<FeedbackDto> GetFeedbackByInterviewIdAsync(Guid interviewId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/{interviewId}/feedback");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FeedbackDto>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null; // Retorna null se não houver feedback encontrado
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: HTTP {response.StatusCode} - Getting Feedback for Interview ID: {interviewId}. Response: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching feedback: {ex.Message}");
                throw; 
            }
        }

        public async Task SubmitFeedbackAsync(FeedbackDto feedbackDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/feedback", feedbackDto);
                response.EnsureSuccessStatusCode(); 

                var newFeedback = new FeedbackDto
                {
                    InterviewId = feedbackDto.InterviewId,
                    Message = feedbackDto.Message,
                    Date = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting feedback: {ex.Message}");
                throw;
            }
        }
    }
}
