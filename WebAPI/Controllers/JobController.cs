using Common.Dtos.Job;
using Common.Dtos.Profile;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]    
[ApiController]
public class JobController(
    IJobRepository _jobRepository,
    IJobSkillRepository _jobSkillRepository): ControllerBase
{ 

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
            // Log the full exception including the inner exception
            Console.WriteLine($"Error creating job: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating job: {ex.Message} {ex.InnerException?.Message}");
        }
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
    public async Task<IActionResult> GetJobs()
    {
        try
        {
            var jobs = await _jobRepository.GetJobsAsync();
            var jobsDto = jobs.JobsConvertToDto();
            return Ok(jobsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving jobs: {ex.Message}");
        }
    }
    
    
     
    [HttpPost("GetClients")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            var jobs = await _jobRepository.GetClients();
            return Ok(jobs.Select(c => c.ClientConvertToDto()));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving jobs: {ex.Message}");
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
    
    [HttpPost("CreateJobSkill")]
    [ProducesResponseType(201, Type = typeof(JobSkillDto))] 
    [ProducesResponseType(400)] 
    public async Task<IActionResult> CreateJobSkill([FromBody] JobSkillDto jobSkillDto) 
    { 
        try
        {
            if (jobSkillDto == null)
            {
                return BadRequest("JobSkill details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobSkill = jobSkillDto.DtoConvertTOJobSkill();
            await _jobSkillRepository.AddJobSkillAsync(jobSkill);

            var createdJobSkillDto = jobSkill.JobSkillConvertToDto();

            return CreatedAtAction(nameof(GetJobSkillsById), new { jobId = createdJobSkillDto.JobId }, createdJobSkillDto);
        }
        catch (Exception ex)
        {
            // Log the full exception including the inner exception
            Console.WriteLine($"Error creating jobSkill: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating jobSkill: {ex.Message} {ex.InnerException?.Message}");
        }
    }
    
    [HttpGet("GetJobSkills")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<JobSkillDto>))]
    public async Task<IActionResult> GetJobSkills()
    {
        try
        {
            var jobSkills = await _jobSkillRepository.GetJobSkillsAsync();
            var jobSkillsDto = jobSkills.JobSkillsConvertToDto();
            return Ok(jobSkillsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving jobSkills: {ex.Message}");
        }
    }
    
    [HttpGet("GetJobSkillsById/{jobId:guid}/{skillId:guid}")]
    [ProducesResponseType(200, Type = typeof(JobSkillDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetJobSkillsById(Guid jobId, Guid skillId)
    {
        try
        {
            var jobSkill = await _jobSkillRepository.GetJobSkillsByIdAsync(jobId, skillId);
            if (jobSkill == null)
            {
                return NotFound();
            }

            var jobSkillDto = jobSkill.JobSkillConvertToDto();
            return Ok(jobSkillDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving jobSkills: {ex.Message}");
        }
    }
}

