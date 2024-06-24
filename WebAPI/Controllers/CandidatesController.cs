using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories.Contracts;

namespace ESOF.WebApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController(ICandidateRepository _candidateRepository) : ControllerBase
{

    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CandidateDto>))]
    public async Task<IActionResult> GetCandidates()
    {
        try
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return Ok(candidates.Select(c => c.CandidateConvertToDto()));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving candidates: {e.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CandidateDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CandidateDto>> CreateCandidate(CandidateDto candidateDto)
    {
        try
        {
            //Ver se existe já um candidato com este nome
            var existingCandidates = await _candidateRepository.GetAllAsync();
            if (existingCandidates.Any(c => string.Equals(c.Name, candidateDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest($"A candidate with the name {candidateDto.Name} already exists.");
            }

            var candidate = new Candidate
            {
                Name = candidateDto.Name
            };

            await _candidateRepository.AddAsync(candidate);
            return CreatedAtAction(nameof(GetCandidateById), new { id = candidate.CandidateId }, candidate.CandidateConvertToDto());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating candidate: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidateDto>> GetCandidateById(Guid id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }
        return Ok(candidate.CandidateConvertToDto());
    }
}
