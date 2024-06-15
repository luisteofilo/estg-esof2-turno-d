using Common.Dtos.Job;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{ 
    private readonly IJobRepository _jobRepository;

    public JobController(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    [HttpPost] 
    [ProducesResponseType(201, Type = typeof(JobDto))] 
    [ProducesResponseType(400)] 
    public async Task<IActionResult> CreateJob([FromBody] JobDto jobDto) 
    { 
        try
        {
            if (jobDto == null)
            {
                return BadRequest("Job details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = jobDto.DtoConvertToJob();
            await _jobRepository.AddJobAsync(job);

            var createdJobDto = job.JobConvertToDto();

            return CreatedAtAction(nameof(GetJobById), new { jobId = createdJobDto.JobId }, createdJobDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating job: {ex.Message}");
        }
    }
    
    [HttpGet("{jobId:guid}")]
    [ProducesResponseType(200, Type = typeof(JobDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetJobById(Guid jobId)
    {
        try
        {
            var job = await _jobRepository.GetJobByIdAsync(jobId);
            if (job == null)
            {
                return NotFound();
            }

            var jobDto = job.JobConvertToDto();
            return Ok(jobDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving job: {ex.Message}");
        }
    }
}

