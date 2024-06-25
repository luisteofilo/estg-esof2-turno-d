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
        // ir buscar o job
        var job = await _context.Jobs.FindAsync(jobId);
        if (job == null)
        {
            throw new Exception("Job not found");
        }

        // Criar uma nova posição
        var position = new Position
        {
            JobId = job.JobId,
            StartDate = startDate,
            EndDate = endDate,
            BillingType = billingType
        };

        // adicionar uma nova posição à bd
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        // criar temesheet inicial
        CreateInitialTimesheets(position);

        return position;
    }

    private void CreateInitialTimesheets(Position position)
    {
        // criar timesheet para a position
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