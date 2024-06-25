
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewFeedbackController : ControllerBase
    {
        private readonly iInterviewFeedbackRepository _interviewFeedbackRepository;

        public InterviewFeedbackController(iInterviewFeedbackRepository interviewFeedbackRepository)
        {
            _interviewFeedbackRepository = interviewFeedbackRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedback>))]
        public async Task<IActionResult> GetInterviewsFeedback()
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewsFeedbackAsync();
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profiles: {ex.Message}");
            }
        }

        [HttpGet("{InterviewFeedbackId:guid}")]
        [ProducesResponseType(200, Type = typeof(InterviewFeedback))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedback(Guid InterviewFeedbackId)
        {
            try
            {
                if (!await _interviewFeedbackRepository.InterviewFeedbackExistsAsync(InterviewFeedbackId))
                {
                    return NotFound();
                }

                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackAsync(InterviewFeedbackId);
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(InterviewFeedback))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateInterviewFeedback([FromBody] InterviewFeedback interviewFeedback)
        {
            try
            {
                if (interviewFeedback == null)
                {
                    return BadRequest("InterviewFeedback details are null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _interviewFeedbackRepository.AddInterviewFeedbackAsync(interviewFeedback);
                return CreatedAtAction(nameof(GetInterviewFeedback), new { InterviewFeedbackId = interviewFeedback.InterviewFeedbackId }, interviewFeedback);
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
        public async Task<IActionResult> UpdateInterviewFeedback(Guid InterviewFeedbackId, [FromBody] InterviewFeedback interviewFeedbackUpdated)
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

                await _interviewFeedbackRepository.UpdateInterviewFeedbackAsync(interviewFeedbackUpdated);
                return NoContent();
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedback>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByJob(Guid jobId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByJob(jobId);
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpGet("candidate/{candidateId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedback>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByCandidate(Guid candidateId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByCandidate(candidateId);
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpGet("interviewer/{interviewerId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedback>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByInterviewer(Guid interviewerId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByInterviewer(interviewerId);
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }

        [HttpGet("interview/{interviewId:guid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewFeedback>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetInterviewFeedbackByInterview(Guid interviewId)
        {
            try
            {
                var interviewFeedback = await _interviewFeedbackRepository.GetInterviewFeedbackByInterview(interviewId);
                return Ok(interviewFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
            }
        }
    }
}
