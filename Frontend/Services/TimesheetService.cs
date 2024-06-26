using Common.Dtos.Job;
using Common.Dtos.Position;
using Common.Dtos.Timesheet;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class TimesheetService(HttpClient _httpClient) : ITimesheetService
{
    public async Task<IEnumerable<TimesheetResponseDTO>> GetAllTimesheets()
    {
        var response = await _httpClient.GetAsync("api/Timesheets");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<TimesheetResponseDTO>>();
    }
}