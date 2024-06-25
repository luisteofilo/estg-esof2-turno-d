using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewFeedbackController : ControllerBase
    {
        private readonly IInterviewFeedback _interviewFeedbackRepository;
        private readonly IJobRepository _jobRepository;

        public InterviewFeedbackController(IInterviewFeedback interviewFeedbackRepository, IJobRepository jobRepository)
        {
            _interviewFeedbackRepository = interviewFeedbackRepository ??
                                           throw new ArgumentNullException(nameof(interviewFeedbackRepository));
            _jobRepository = jobRepository ??
                             throw new ArgumentNullException(nameof(jobRepository));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        public async Task<IActionResult> GetInterviewsFeedback()
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewsFeedbackAsync();
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();
                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpGet("{interviewFeedbackId:guid}")]
        [ProducesResponseType(200, Type = typeof(InterviewFeedbackDTO))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedback(Guid interviewFeedbackId)
        {
            try
            {
                var interviewFeedback =
                    await _interviewFeedbackRepository.GetInterviewFeedbackAsync(interviewFeedbackId);

                if (interviewFeedback == null)
                {
                    return NotFound();
                }

                var interviewFeedbackDto = interviewFeedback.InterviewFeedbackConvertToDto();
                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(InterviewFeedbackDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateInterviewFeedback([FromBody] InterviewFeedbackDTO interviewFeedbackDto)
        {
            try
            {
                if (interviewFeedbackDto == null)
                {
                    return BadRequest("Interview feedback details are null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var interviewFeedback = interviewFeedbackDto.DtoConvertToInterviewFeedback();
                await _interviewFeedbackRepository.AddInterviewFeedbackAsync(interviewFeedback);

                var createdInterviewFeedbackDto = interviewFeedback.InterviewFeedbackConvertToDto();

                return CreatedAtAction(nameof(GetInterviewFeedback),
                    new { interviewFeedbackId = createdInterviewFeedbackDto.InterviewFeedbackId },
                    createdInterviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error creating interview feedback: {ex.Message}");
            }
        }

        [HttpPut("{interviewFeedbackId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateInterviewFeedback(Guid interviewFeedbackId,
            [FromBody] InterviewFeedbackDTO interviewFeedbackDto)
        {
            try
            {
                if (interviewFeedbackDto == null || interviewFeedbackDto.InterviewFeedbackId != interviewFeedbackId)
                {
                    return BadRequest("Interview feedback ID mismatch or details are null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(interviewFeedbackId))
                {
                    return NotFound();
                }

                var updatedInterviewFeedback = interviewFeedbackDto.DtoConvertToInterviewFeedback();
                await _interviewFeedbackRepository.UpdateInterviewFeedbackAsync(updatedInterviewFeedback);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating interview feedback: {ex.Message}");
            }
        }

        [HttpDelete("{interviewFeedbackId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteInterviewFeedback(Guid interviewFeedbackId)
        {
            try
            {
                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(interviewFeedbackId))
                {
                    return NotFound();
                }

                await _interviewFeedbackRepository.DeleteInterviewFeedbackAsync(interviewFeedbackId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting interview feedback: {ex.Message}");
            }
        }

        [HttpGet("job/{jobId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByJob(Guid jobId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByJob(jobId);
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpGet("candidate/{candidateId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByCandidate(Guid candidateId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByCandidate(candidateId);
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpGet("interviewer/{interviewerId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByInterviewer(Guid interviewerId)
        {
            try
            {
                var interviewFeedback =
                    await _interviewFeedbackRepository.GetInterviewFeedbackByInterviewer(interviewerId);
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpGet("interview/{interviewId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByInterview(Guid interviewId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByInterview(interviewId);
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving interview feedback: {ex.Message}");
            }
        }

        [HttpPut("update-job/{jobId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateJob(Guid jobId, [FromBody] JobDto jobDto)
        {
            try
            {
                if (jobDto == null || jobDto.JobId != jobId)
                {
                    return BadRequest("Job ID mismatch or details are null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingJob = await _jobRepository.GetJobByIdAsync(jobId);
                if (existingJob == null)
                {
                    return NotFound(new { message = "Job not found." });
                }

                // Update the existing job with the new values
                existingJob.ClientId = jobDto.ClientId;
                existingJob.EndDate = jobDto.EndDate;
                existingJob.Position = jobDto.Position;
                existingJob.Commitment = jobDto.Commitment;
                existingJob.Remote = jobDto.Remote;
                existingJob.Localization = jobDto.Localization;
                existingJob.Education = jobDto.Education;
                existingJob.Experience = jobDto.Experience;
                existingJob.Description = jobDto.Description;

                await _jobRepository.UpdateJobAsync(existingJob);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating job: {ex.Message}");
            }
        }


    }
}
