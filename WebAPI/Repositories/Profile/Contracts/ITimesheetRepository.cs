using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ITimesheetRepository
{
    Task CreateTimesheet(Timesheet timesheet);

    // Get all Timesheets
    Task<IEnumerable<Timesheet>> GetAllTimesheets();

    // Get a Timesheet by ID
    Task<Timesheet> GetTimesheetById(Guid id);
    
    // Update a Timesheet
    Task UpdateTimesheet(Timesheet timesheet);
    
    // Delete a Timesheet
    Task<bool> DeleteTimesheet(Timesheet timesheet);

}