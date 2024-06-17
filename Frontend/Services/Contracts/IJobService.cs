using Common.Dtos.Job;

namespace Frontend.Services.Contracts;

public interface IJobService
{
    Task<JobDto> CreateJob(Guid JobId, Guid ClientId, JobDto jobDto);
}