using System.Runtime.InteropServices;
using Common.Dtos.Job;
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
    [HttpGet("profile")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetSearchResultsSkillsLocation([FromQuery] string firstName,[FromQuery] string skill = null,[FromQuery] string location = null)
    {
        try
        {
            if (!await searchRepository.ProfileExistsAsync(firstName))
            {
                return NotFound();
            }

            var result = await searchRepository.GetSearchResultsAsync(firstName, skill, location);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("locations/")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
    public async Task<IActionResult> s()
    {
        try
        {
            var result = await searchRepository.GetLocationsAsync(); ;
            
            return Ok(result);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }

    
  /*  [HttpGet("{firstName}")]
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
    
    [HttpGet("{firstName}/skills/{skill}")]
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
    
    [HttpGet("{firstName}/locations/{location}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult>GetSearchResultsLocation(string firstName, string location)
    {
        try
        {
            if (!await searchRepository.ProfileExistsAsync(firstName))
            {
                return NotFound();
            }

            var result = await searchRepository.GetSearchResultsLocationAsync(firstName, location);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("locations/")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
    public async Task<IActionResult> s()
    {
        try
        {
            var result = await searchRepository.GetLocationsAsync(); ;
            
            return Ok(result);
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
    
    [HttpGet("skills/{skill}/locations/{location}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetResultsLocationSkill(string location, string skill)
    {
        try
        {
            var result = await searchRepository.GetResultsLocationSkillAsync(location, skill);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("{firstName}/skills/{skill}/locations/{location}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetSearchResultsSkillsLocation(string firstName, string skill, string location)
    {
        try
        {
            if (!await searchRepository.ProfileExistsAsync(firstName))
            {
                return NotFound();
            }

            var result = await searchRepository.GetSearchResultsSkillsLocationAsync(firstName, skill, location);
            var profileDto = result.ProfilesConvertToDto();
            
            return Ok(profileDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("jobs/skills/{skill}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
    public async Task<IActionResult>GetResultsJobBySkillAsync(string skill)
    {
        try
        {
            var result = await searchRepository.GetJobBySkillAsync(skill);
            var jobDto = result.JobsConvertToDto();
            
            return Ok(jobDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    
    [HttpGet("jobs/locations/{location}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
    public async Task<IActionResult>GetResultsJobByLocationAsync(string location)
    {
        try
        {
            var result = await searchRepository.GetJobByLocationAsync(location);
            var jobDto = result.JobsConvertToDto();
            
            return Ok(jobDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    
    [HttpGet("jobs/positions/{position}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
    public async Task<IActionResult>GetResultsJobByPositionAsync(string position)
    {
        try
        {
            var result = await searchRepository.GetJobByPositionAsync(position);
            var jobDto = result.JobsConvertToDto();
            
            return Ok(jobDto);
        } catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }
    */
}