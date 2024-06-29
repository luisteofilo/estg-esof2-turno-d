using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using Hangfire;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Persistence;
using System.Collections.Generic;

namespace ESOF.WebApp.WebAPI.Services
{
    public class ExternalJobService
    {
        private readonly IExternalJobRepository _jobRepository;
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ScraperFactory _scraper;

        public ExternalJobService(
            IExternalJobRepository jobRepository,
            IImportRepository importRepository,
            IUnitOfWork unitOfWork,
            ScraperFactory scraper)
        {
            _jobRepository = jobRepository;
            _importRepository = importRepository;
            _unitOfWork = unitOfWork;
            _scraper = scraper;
        }

        public async Task<Job> CreateExternalJob(string url, JobResult request, CancellationToken cancellationToken)
        {
            var import = new Import
            {
                Url = url,
            };

            await _importRepository.Create(import, cancellationToken);

            var job = new Job
            {
                ClientId = Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0178D"), // TODO: Replace with the actual client ID
                Localization = request.Location,
                Description = request.Content,
                Company = request.Company,
                OtherDetails = request.OtherDetails,
                Import = import,
                Positions = new List<Position>
                {
                    new Position
                    {
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        BillingType = "Monthly",
                        JobId = Guid.NewGuid() 
                    }
                }
            };

            await _jobRepository.Create(job, cancellationToken);

            import.Jobs.Add(job);

            return job;
        }

        public async Task<Job> UpdateExternalJob(Job importedJob, JobResult request, CancellationToken cancellationToken)
        {
            importedJob.Localization = request.Location;
            importedJob.Description = request.Content;
            importedJob.Company = request.Company;
            importedJob.OtherDetails = request.OtherDetails;
            importedJob.UpdatedAt = DateTimeOffset.UtcNow;
            importedJob.Import!.UpdatedAt = DateTimeOffset.UtcNow;

            if (importedJob.Positions == null)
            {
                importedJob.Positions = new List<Position>
                {
                    new Position
                    {
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        BillingType = "Monthly",
                        JobId = importedJob.JobId
                    }
                };
            }
            else
            {
                foreach (var position in importedJob.Positions)
                {
                    position.BillingType = "Monthly";
                    position.StartDate = DateTime.UtcNow;
                    position.EndDate = DateTime.UtcNow.AddMonths(1);
                }
            }

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
}
