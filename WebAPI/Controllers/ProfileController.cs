using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IProfileRepository profileRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        try
        {
            var profile = await profileRepository.GetProfile();
            
            if(profile == null)
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