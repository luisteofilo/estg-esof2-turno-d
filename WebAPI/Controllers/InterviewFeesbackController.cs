
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewFeedbackController : ControllerBase
    {
        private readonly IInterviewFeedback _interviewFeedbackRepository;
        private readonly JobRepository jobRepository;


        public InterviewFeedbackController(IInterviewFeedback interviewFeedbackRepository)
        {
            _interviewFeedbackRepository = interviewFeedbackRepository;
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profiles: {ex.Message}");
            }
        }

        [HttpGet("{InterviewFeedbackId:guid}")]
        [ProducesResponseType(200, Type = typeof(InterviewFeedbackDTO))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedback(Guid interviewFeedbackId)
        {
            try
            {
                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(interviewFeedbackId))
                {
                    return NotFound();
                }

                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackAsync(interviewFeedbackId);
                var interviewFeedbackDto = interviewFeedback.InterviewFeedbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(InterviewFeedbackDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProfile([FromBody] InterviewFeedbackDTO interviewFeedbackDto)
        {
            try
            {
                if (interviewFeedbackDto == null)
                {
                    return BadRequest("Profile details are null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var interviewFeedback = interviewFeedbackDto.DtoConvertToInterviewFeedback();
                await _interviewFeedbackRepository.AddInterviewFeedbackAsync(interviewFeedback);

                var createdInterviewFeedback = interviewFeedback.InterviewFeedbackConvertToDto();

                return CreatedAtAction(nameof(GetInterviewFeedback), new { interviewFeedbadId = createdInterviewFeedback.InterviewFeedbackId }, createdInterviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating profile: {ex.Message}");
            }
        }

        [HttpPut("{InterviewFeedbackId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateInterviewFeedback(Guid InterviewFeedbackId, [FromBody] InterviewFeedbackDTO interviewFeedbackUpdated)
        {
            try
            {
                if (interviewFeedbackUpdated == null || interviewFeedbackUpdated.InterviewFeedbackId != InterviewFeedbackId)
                {
                    return BadRequest(ModelState);
                }

                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(InterviewFeedbackId))
                {
                    return NotFound();
                }
                var updatedInterviewFeedback = interviewFeedbackUpdated.DtoConvertToInterviewFeedback();

                await _interviewFeedbackRepository.UpdateInterviewFeedbackAsync(updatedInterviewFeedback);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile: {ex.Message}");
            }
        }
        
        
        
        [HttpPut("{JobId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateJob(Guid JobId, [FromBody] JobDto jobDto)
        {
            try
            {
                if (jobDto == null || jobDto.JobId != JobId)
                {
                    return BadRequest(ModelState);
                }

                
                var updatedProfile = jobDto.DtoConvertToJob();
                await jobRepository.UpdateJobAsync(updatedProfile);

                return Ok(jobDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile: {ex.Message}");
            }
        }
        
        

        [HttpDelete("{InterviewFeedbackId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteInterviewFeedback(Guid InterviewFeedbackId)
        {
            try
            {
                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(InterviewFeedbackId))
                {
                    return NotFound();
                }

                await _interviewFeedbackRepository.DeleteInterviewFeedbackAsync(InterviewFeedbackId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting profile: {ex.Message}");
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpGet("interviewer/{interviewerId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedbackDTO>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByInterviewer(Guid interviewerId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByInterviewer(interviewerId);
                var interviewFeedbackDto = interviewFeedback.InterviewsçFeesbackConvertToDto();

                return Ok(interviewFeedbackDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }
    }
}
