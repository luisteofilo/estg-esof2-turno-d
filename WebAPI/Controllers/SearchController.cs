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
            var profile = await profileRepository.GetProfile();

            if (profile == null)
            {
                return NotFound();
            }

            var eventDto = profile.ToDto();
            return Ok(eventDto);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }

    }
}