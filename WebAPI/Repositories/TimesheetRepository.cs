using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    // Create a new Timesheet
    public async Task CreateTimesheet(Timesheet timesheet)
    {
        _context.Timesheets.Add(timesheet);
        await _context.SaveChangesAsync();
    }

    // Get all timesheets
    public async Task<IEnumerable<Timesheet>> GetAllTimesheets()
    {
        return await _context.Timesheets.Include(t => t.Position).ToListAsync();
    }

    // Get a timesheet by ID
    public async Task<Timesheet> GetTimesheetById(Guid id)
    {
        return await _context.Timesheets.FindAsync(id);
    }

    // Update a timesheet
    public async Task UpdateTimesheet(Timesheet timesheet)
    {
        _context.Timesheets.Update(timesheet);
        await _context.SaveChangesAsync();
    }

    // Delete a timesheet
    public async Task<bool> DeleteTimesheet(Timesheet timesheet)
    {
        _context.Timesheets.Remove(timesheet);
        await _context.SaveChangesAsync();
        return true;
    }
}