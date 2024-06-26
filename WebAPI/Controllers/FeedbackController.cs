using Common.Dtos.Feedback;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController(IFeedbackRepository feedbackRepository) : ControllerBase
    {

        [HttpGet("{interviewId}/feedback")]
        public async Task<ActionResult<FeedbackDto>> GetFeedback(Guid interviewId)
        {
            try
            {
                var feedback = await feedbackRepository.GetFeedbackByInterviewIdAsync(interviewId);

                if (feedback == null)
                {
                    return NotFound();
                }

                var feedbackDto = feedback.ToDto();
                return Ok(feedbackDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            try
            {
                if (feedbackDto == null)
                {
                    return BadRequest();
                }

                var feedback = new Feedback
                {
                    FeedbackId = Guid.NewGuid(),
                    Message = feedbackDto.Message,
                    Date = DateTime.UtcNow,
                    InterviewId = feedbackDto.InterviewId
                };

                await feedbackRepository.AddFeedbackAsync(feedback);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error saving data to the database.");
            }
        }
    }
}
