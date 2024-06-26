using Common.Dtos.Jobs;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;

public class JobPositionService
{
    private readonly ApplicationDbContext _context;
    private readonly IJobRepository _jobRepository;
    private readonly IPositionRepository _positionRepository;

    public JobPositionService(ApplicationDbContext context, IJobRepository jobRepository, IPositionRepository positionRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
        _positionRepository = positionRepository;
    }
    
    public async Task<Position> ConvertJobToPosition(Guid jobId, JobToPositionConvertDTO dto)
    {
        // ir buscar o job
        var job = await _jobRepository.GetJobByIdAsync(jobId);
        if (job == null)
        {
            throw new Exception("Job not found");
        }

        // Criar uma nova posição
        var position = new Position
        {
            JobId = job.JobId,
            StartDate = DateTime.Now.ToUniversalTime(),
            EndDate = dto.EndDate.ToUniversalTime(),
            BillingType = dto.BillingType
        };

        // adicionar uma nova posição à bd
        await _positionRepository.CreatePosition(position);

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