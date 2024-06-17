
using Common.Dtos.Profile;

using Microsoft.AspNetCore.Mvc;
using ESOF.WebApp.WebAPI.Repositories.Contracts;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class SearchController(IProfileRepository profileRepository) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetResults()
    {
        try
        {

            var profile = await profileRepository.GetProfilesAsync();



            if (profile == null)
            {
                return NotFound();
            }
            
            var eventDto = profile.ProfilesConvertToDto();

            return Ok(eventDto);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }

    }
}