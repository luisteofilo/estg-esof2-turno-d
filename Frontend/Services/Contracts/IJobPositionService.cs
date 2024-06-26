using Common.Dtos.Jobs;

namespace Frontend.Services.Contracts;

public interface IJobPositionService
{
    Task ConvertJobToPosition(Guid jobId, JobToPositionConvertDTO request);
}