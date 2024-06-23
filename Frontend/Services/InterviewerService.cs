using Common.Dtos.Interview;
using Frontend.Services.Contracts;

namespace Frontend.Services;
public class InterviewerService(HttpClient _httpClient) : IInterviewerService
{
    public async Task<IEnumerable<InterviewerDto>> GetInterviewersAsync()
    {
        var response = await _httpClient.GetAsync("api/Interviewers");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<InterviewerDto>>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Getting Interviewers");
        }
    }

    public async Task CreateInterviewerAsync(InterviewerDto interviewer)
    {
        // Isto serve para ver se um Interviewer com o mesmo nome já existe
        var interviewers = await GetInterviewersAsync();
        if (interviewers.Any(i => string.Equals(i.Name, interviewer.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new HttpRequestException($"An interviewer with the name {interviewer.Name} already exists.");
        }

        var response = await _httpClient.PostAsJsonAsync("api/Interviewers", interviewer);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Creating Interviewer");
        }
    }
}
