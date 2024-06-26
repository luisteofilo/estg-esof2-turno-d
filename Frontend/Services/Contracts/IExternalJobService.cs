using Common.Contracts.Job;

namespace Frontend.Services.Contracts;

public interface IExternalJobService
{
    Task<ExternalJobResponse> CreateExternalJob(string url);
}