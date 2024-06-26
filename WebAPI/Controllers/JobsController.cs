using Common.Dtos.Jobs;
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly JobPositionService _jobPositionService;

    public JobsController(JobPositionService jobPositionService)
    {
        _jobPositionService = jobPositionService;
    }

    // [HttpPost("{jobId}/convert")]
    // public async Task<IActionResult> ConvertJobToPosition(int jobId, [FromBody] ConvertJobToPositionRequest request)
    // {
    //     try
    //     {
    //         var position = await _jobPositionService.ConvertJobToPosition(jobId, request.StartDate, request.EndDate, request.BillingType);
    //         return Ok(position);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }
    
    [HttpPost("{jobId}/ConvertToPosition")]
    public async Task<IActionResult> ConvertJobToPosition(Guid jobId, [FromBody] JobToPositionConvertDTO request)
    {
        try
        {
            var position = await _jobPositionService.ConvertJobToPosition(jobId, request);
            return Ok(position);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class ConvertJobToPositionRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BillingType { get; set; }
}

