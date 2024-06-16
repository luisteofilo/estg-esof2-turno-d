using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using ESOF.WebApp.WebAPI.Errors;

namespace ESOF.WebApp.WebAPI.Services;

public class ExternalJobService(IJobRepository _jobRepository, IUnitOfWork _unitOfWork, ScraperFactory _scraper)
{
    public async Task<Job> AddExternalJobAsync(string url, CancellationToken cancellationToken)
    {
        url = url.Trim();

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new NullUrlException();
        }

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new FormatUrlException();
        }

        try
        {
            IScraper<JobResult> scraper = _scraper.CreateScraper(new(url));

            var request = await scraper.Handle(url);

            var job = new Job
            {
                Title = request.Title,
                Location = request.Location,
                Content = request.Content,
                Company = request.Company,
                OtherDetails = request.OtherDetails,
            };

            await _jobRepository.Create(job);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return job;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
