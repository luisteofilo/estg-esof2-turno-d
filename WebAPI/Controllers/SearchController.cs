using System.Runtime.InteropServices;
using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc; 

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class SearchController(ISearchRepository searchRepository) : ControllerBase
{

    [HttpGet("{firstName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetSearchResults(string firstName)
    {
        try
        {
            if (!await searchRepository.ProfileExistsAsync(firstName))
            {
                return NotFound();
            }

            var result = await searchRepository.GetSearchResultsAsync(firstName);
            var profileDto = result.ProfilesConvertToDto();

            return Ok(profileDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("skills/{skill}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetResultsSkills(string skill)
    {
        try
        {
            
            var result = await searchRepository.GetResultsSkillsAsync(skill);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("locations/{location}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetResultsLocation(string location)
    {
        try
        {
            
            var result = await searchRepository.GetResultsLocationAsync(location);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }


    [HttpGet("{firstName}/{skill}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetSearchResultsSkills(string firstName, string skill)
    {
        try
        {
            if (!await searchRepository.ProfileExistsAsync(firstName))
            {
                return NotFound();
            }

            var result = await searchRepository.GetSearchResultsSkillsAsync(firstName, skill);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
}