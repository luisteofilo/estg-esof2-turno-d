using Common.Dtos.Job;
using Common.Dtos.Profile;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobSkillRepository _jobSkillRepository;

        public JobController(IJobRepository jobRepository, IJobSkillRepository jobSkillRepository)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _jobSkillRepository = jobSkillRepository ?? throw new ArgumentNullException(nameof(jobSkillRepository));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JobDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JobDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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



        [HttpGet("{jobId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JobSkillDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

                return CreatedAtAction(nameof(GetJobSkillsById), new { jobId = createdJobSkillDto.JobId, skillId = createdJobSkillDto.SkillId }, createdJobSkillDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating jobSkill: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating jobSkill: {ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpGet("GetJobSkills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JobSkillDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobSkillDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving jobSkill: {ex.Message}");
            }
        }
    }
}
