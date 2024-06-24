using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories.Contracts;
namespace ESOF.WebApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewsController(IInterviewRepository _interviewRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewDto>))]
    public async Task<IActionResult> GetInterviews()
    {
        try
        {
            var interviews = await _interviewRepository.GetAllAsync();
            var interviewsDto = interviews.InterviewsConvertToDto();
            return Ok(interviewsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving interviews: {ex.Message}");
        }
    }

    [HttpGet("{interviewId:guid}")]
    [ProducesResponseType(200, Type = typeof(InterviewDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetInterview(Guid interviewId)
    {
        try
        {
            if (!await _interviewRepository.InterviewExistsAsync(interviewId))
            {
                return NotFound();
            }

            var interview = await _interviewRepository.GetByIdAsync(interviewId);
            var interviewDto = interview.InterviewConvertToDto();

            return Ok(interviewDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving interview: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(InterviewDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateInterview([FromBody] InterviewDto interviewDto)
    {
        try
        {
            //Aqui estão todas as verificações necessarias para criar uma entrevista sem problemas 
            if (interviewDto == null)
            {
                return BadRequest("Interview details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime minimumStartTime = DateTime.UtcNow.AddHours(1);
            if (interviewDto.DateHourStart < minimumStartTime)
            {
                return BadRequest($"Interview start time must be at least 1 hour from now. The earliest possible start time is {minimumStartTime}.");
            }

            if (interviewDto.DateHourEnd.Date != interviewDto.DateHourStart.Date)
            {
                return BadRequest("Interview end time must be on the same day as the start time.");
            }

            TimeSpan duration = interviewDto.DateHourEnd - interviewDto.DateHourStart;
            if (duration < TimeSpan.FromMinutes(30) || duration > TimeSpan.FromMinutes(90))
            {
                return BadRequest("Interview duration must be between 30 minutes and 1 hour and 30 minutes.");
            }

            //Resolução do Erro da estrutura da Data
            interviewDto.DateHourStart = DateTime.SpecifyKind(interviewDto.DateHourStart, DateTimeKind.Utc);
            interviewDto.DateHourEnd = DateTime.SpecifyKind(interviewDto.DateHourEnd, DateTimeKind.Utc);

            if (!await _interviewRepository.InterviewerExistsAsync(interviewDto.InterviewerId))
            {
                return BadRequest("Interviewer does not exist.");
            }
            
            if (!await _interviewRepository.CandidateExistsAsync(interviewDto.CandidateId))
            {
                return BadRequest("Candidate does not exist.");
            }

            if (!await _interviewRepository.IsInterviewerAndCandidateAvailableAsync(interviewDto.InterviewerId, interviewDto.CandidateId, interviewDto.DateHourStart, interviewDto.DateHourEnd))
            {
                return BadRequest("The interviewer or candidate has a conflicting interview or does not have a 30-minute break between interviews.");
            }

            var interview = interviewDto.DtoConvertToInterview();
            interview.InterviewId = Guid.NewGuid();
            interview.InterviewState = InterviewState.Scheduled;

            await _interviewRepository.AddAsync(interview);

            var createdInterviewDto = interview.InterviewConvertToDto();

            return CreatedAtAction(nameof(GetInterview), new { interviewId = createdInterviewDto.InterviewId }, createdInterviewDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating interview: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpPut("{interviewId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateInterview(Guid interviewId, [FromBody] InterviewDto updatedInterviewDto)
    {
        try
        {
            if (updatedInterviewDto == null || updatedInterviewDto.InterviewId != interviewId)
            {
                return BadRequest(ModelState);
            }

            if (!await _interviewRepository.InterviewExistsAsync(interviewId))
            {
                return NotFound();
            }

            var updatedInterview = updatedInterviewDto.DtoConvertToInterview();
            await _interviewRepository.UpdateAsync(updatedInterview);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating interview: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpPut("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelInterview(Guid id)
    {
        try
        {
            var interview = await _interviewRepository.GetByIdAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            interview.InterviewState = InterviewState.Canceled;
            await _interviewRepository.UpdateAsync(interview);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error canceling interview: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpPut("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteInterview(Guid id)
    {
        try
        {
            var interview = await _interviewRepository.GetByIdAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            interview.InterviewState = InterviewState.Completed;
            await _interviewRepository.UpdateAsync(interview);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error completing interview: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpPut("{id}/missed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MissInterview(Guid id)
    {
        try
        {
            var interview = await _interviewRepository.GetByIdAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            interview.InterviewState = InterviewState.Missed;
            await _interviewRepository.UpdateAsync(interview);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error miss state interview: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{interviewId:guid}/currentStatus")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult IsCurrentDateTimeWithinInterview(Guid interviewId)
    {
        try
        {
            var isWithinInterview = _interviewRepository.IsCurrentDateTimeWithinInterview(interviewId);
            return Ok(new { IsWithinInterview = isWithinInterview });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error checking interview status: {ex.Message}");
        }
    }

    [HttpGet("currentDateTime")]
    [ProducesResponseType(200)]
    public IActionResult GetCurrentDateTime()
    {
        var currentDateTime = DateTime.Now;
        return Ok(new { CurrentDateTime = currentDateTime });
    }

    [HttpPut("{interviewId:guid}/markPresence")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkPresence(Guid interviewId)
    {
        try
        {
            if (!await _interviewRepository.InterviewExistsAsync(interviewId))
            {
                return NotFound();
            }

            await _interviewRepository.MarkPresenceAsync(interviewId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error marking presence: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    [HttpPut("{interviewId:guid}/updateState")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInterviewState(Guid interviewId, [FromQuery] InterviewState state)
    {
        try
        {
            if (!await _interviewRepository.InterviewExistsAsync(interviewId))
            {
                return NotFound();
            }

            await _interviewRepository.UpdateInterviewStateAsync(interviewId, state);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating interview state: {ex.Message}\n{ex.InnerException?.Message}");
        }
    }
    
    [HttpGet("{interviewId:guid}/presence")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPresenceState(Guid interviewId)
    {
        try
        {
            if (!await _interviewRepository.InterviewExistsAsync(interviewId))
            {
                return NotFound();
            }

            var presenceMarked = await _interviewRepository.GetPresenceStateAsync(interviewId);
            return Ok(presenceMarked);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving presence state: {ex.Message}");
        }
    }

}

