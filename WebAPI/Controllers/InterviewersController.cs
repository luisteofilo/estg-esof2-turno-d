using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories.Contracts;

namespace ESOF.WebApp.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InterviewersController(IInterviewerRepository _interviewerRepository) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<InterviewerDto>))]
    public async Task<IActionResult> GetInterviewers()
    {
        try
        {
            var interviewers = await _interviewerRepository.GetAllAsync();
            return Ok(interviewers.Select(c => c.InterviewerConvertToDto()));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving interviewers: {e.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(InterviewerDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<InterviewerDto>> CreateCandidate(InterviewerDto interviewerDto)
    {
        try
        {
            //Ver se existe já um entrevistador com este nome
            var existingInterviewers = await _interviewerRepository.GetAllAsync();
            if (existingInterviewers.Any(c => string.Equals(c.Name, interviewerDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest($"A interviewer with the name {interviewerDto.Name} already exists.");
            }

            var interviewer = new Interviewer
            {
                Name = interviewerDto.Name
            };

            await _interviewerRepository.AddAsync(interviewer);
            return CreatedAtAction(nameof(GetInterviewerById), new { id = interviewer.InterviewerId }, interviewer.InterviewerConvertToDto());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating interviewer: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterviewerDto>> GetInterviewerById(Guid id)
    {
        var interviewer = await _interviewerRepository.GetByIdAsync(id);
        if (interviewer == null)
        {
            return NotFound();
        }
        return Ok(interviewer.InterviewerConvertToDto());
    }
}
