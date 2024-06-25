using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class InterviewService(HttpClient _httpClient) : IInterviewService
{
    public async Task<InterviewDto> CreateInterviewAsync(InterviewDto interviewDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Interviews", interviewDto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<InterviewDto>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Invalid Interview");
        }
    }

    public async Task<IEnumerable<InterviewDto>> GetInterviewsAsync()
    {
        var response = await _httpClient.GetAsync("api/Interviews");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<InterviewDto>>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Getting Interviews");
        }
    }

    public async Task<InterviewDto> GetInterviewByIdAsync(Guid interviewId)
    {
        var response = await _httpClient.GetAsync($"api/Interviews/{interviewId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<InterviewDto>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Getting Interview");
        }
    }

    public async Task CancelInterviewAsync(Guid interviewId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/cancel", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Cancelling Interview");
        }
    }

    public async Task CompleteInterviewAsync(Guid interviewId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/complete", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Completing Interview");
        }
    }

    public async Task MissInterviewAsync(Guid interviewId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/missed", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Marking Interview as Missed");
        }
    }

    public async Task<bool> CheckInterviewAsync(Guid interviewId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/currentStatus", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Checking Interview");
        }

        var result = await response.Content.ReadFromJsonAsync<bool>();
        return result;
    }

    public async Task<DateTime> GetCurrentDateTimeAsync()
    {
        var response = await _httpClient.GetAsync("api/Interviews/currentDateTime");
        if (response.IsSuccessStatusCode)
        {
            var dateTimeResponse = await response.Content.ReadFromJsonAsync<DateTimeResponse>();
            return dateTimeResponse.CurrentDateTime;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Fetching Current Date and Time");
        }
    }

    public async Task MarkPresenceAsync(Guid interviewId)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/markPresence", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Marking Presence");
        }
    }
    
    public async Task UpdateInterviewStateAsync(Guid interviewId, InterviewState state)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Interviews/{interviewId}/updateState?state={state}", new { });
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Updating Interview State");
        }
    }
    
    public async Task<bool> GetPresenceMarkedAsync(Guid interviewId)
    {
        var response = await _httpClient.GetAsync($"api/Interviews/{interviewId}/presence");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Getting Presence State");
        }
    }

    //Serve apenas para guardar a dataHora atual apos refresh, não é uma entidade
    public class DateTimeResponse
    {
        public DateTime CurrentDateTime { get; set; }
    }
}

