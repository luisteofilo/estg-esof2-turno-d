using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using ESOF.WebApp.WebAPI.Errors;
using Hangfire;

namespace ESOF.WebApp.WebAPI.Services;

public class ExternalJobService(IExternalJobRepository _jobRepository, IImportRepository _importRepository, IUnitOfWork _unitOfWork, ScraperFactory _scraper)
{
    public async Task<Job> CreateExternalJob(string url, JobResult request, CancellationToken cancellationToken)
    {
        var import = new Import
        {
            Url = url,
        };

        await _importRepository.Create(import, cancellationToken);

        //TODO: Change client hard coded
        return import.AddJob(Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0178D"), request.Title, request.Location, request.Content, request.Company, request.OtherDetails);
    }

    public async Task<Job> UpdateExternalJob(Job importedJob, JobResult request, CancellationToken cancellationToken)
    {
        importedJob.Positions = request.Title;
        importedJob.Localization = request.Location;
        importedJob.Description = request.Content;
        importedJob.Company = request.Company;
        importedJob.OtherDetails = request.OtherDetails;
        importedJob.UpdatedAt = DateTimeOffset.UtcNow;
        importedJob.Import!.UpdatedAt = DateTimeOffset.UtcNow;

        await _jobRepository.Update(importedJob, cancellationToken);

        return importedJob;
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task<Job> AddExternalJobAsync(string url, CancellationToken cancellationToken)
    {
        url = url.Trim();

        try
        {
            var importedJob = await _jobRepository.GetJobByUrl(url, cancellationToken);

            IScraper<JobResult> scraper = _scraper.CreateScraper(new(url));

            var request = await scraper.Handle(url);

            if (importedJob == null)
            {
                importedJob = await CreateExternalJob(url, request, cancellationToken);
            }
            else
            {
                importedJob = await UpdateExternalJob(importedJob, request, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return importedJob;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
