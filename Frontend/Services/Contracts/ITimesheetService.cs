using Common.Dtos.Timesheet;

namespace Frontend.Services.Contracts;

public interface ITimesheetService
{
    Task<IEnumerable<TimesheetResponseDTO>> GetAllTimesheets();
}