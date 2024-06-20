using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;

public class JobPositionService
{
    private readonly ApplicationDbContext _context;

    public JobPositionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Position> ConvertJobToPosition(int jobId, DateTime startDate, DateTime endDate, string billingType)
    {
        // Retrieve the job
        var job = await _context.Jobs.FindAsync(jobId);
        if (job == null)
        {
            throw new Exception("Job not found");
        }

        // Create a new position
        var position = new Position
        {
            JobId = job.JobId,
            StartDate = startDate,
            EndDate = endDate,
            BillingType = billingType
        };

        // Add the new position to the database
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        // Create initial timesheets for the position (example logic)
        CreateInitialTimesheets(position);

        return position;
    }

    private void CreateInitialTimesheets(Position position)
    {
        // Example logic to create initial timesheets for the position
        for (var date = position.StartDate; date <= position.EndDate; date = date.AddDays(1))
        {
            var timesheet = new Timesheet
            {
                Date = date,
                HoursWorked = 0,
                TaskDescription = "Initial timesheet",
                PositionId = position.PositionId
            };

            _context.Timesheets.Add(timesheet);
        }

        _context.SaveChanges();
    }
}