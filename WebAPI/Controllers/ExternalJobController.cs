using ESOF.WebApp.WebAPI.Contracts.Job;
using ESOF.WebApp.WebAPI.Errors;
using ESOF.WebApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/jobs/external")]
[ApiController]
public class ExternalJobController(ExternalJobService _externalJobService) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<ExternalJobResponse>> AddExternalJob([FromBody] ExternalJobRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var job = await _externalJobService.AddExternalJobAsync(request.Url, cancellationToken);
            return CreatedAtAction(nameof(AddExternalJob), new ExternalJobResponse(job.JobId));
        }
        catch (NullUrlException)
        {
            //TODO: Change
            return BadRequest("URL is required.");
        }
        catch (FormatUrlException)
        {
            //TODO: Change
            return BadRequest("The provided URL format is invalid.");
        }
        catch (Exception)
        {
            //TODO: Change
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}
