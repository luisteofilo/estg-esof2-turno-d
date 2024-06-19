using ESOF.WebApp.WebAPI.Contracts.Job;
using ESOF.WebApp.WebAPI.Errors;
using ESOF.WebApp.WebAPI.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/jobs/external")]
[ApiController]
public class ExternalJobController(ExternalJobService _externalJobService) : ControllerBase
{

    [HttpPost]
    public ActionResult<ExternalJobResponse> AddExternalJob([FromBody] ExternalJobRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var taskId = BackgroundJob.Enqueue(() => _externalJobService.AddExternalJobAsync(request.Url, cancellationToken));
            return CreatedAtAction(nameof(AddExternalJob), new ExternalJobResponse(taskId));
        }
        catch (NullUrlException)
        {
            return BadRequest("URL is required.");
        }
        catch (FormatUrlException)
        {
            return BadRequest("The provided URL format is invalid.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [HttpGet("{taskId}/status")]
    public ActionResult<string> GetJobState(string taskId)
    {
        using var connection = JobStorage.Current.GetConnection();

        var jobData = connection.GetJobData(taskId);
        if (jobData == null)
        {
            return NotFound();
        }

        var stateData = connection.GetStateData(taskId);
        return stateData.Name;

    }
}
