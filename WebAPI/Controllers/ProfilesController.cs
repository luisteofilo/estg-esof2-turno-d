using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController(IProfileRepository profileRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        try
        {
            var profiles = await profileRepository.GetProfiles();
            
            if(profiles == null)
            {
                return NotFound();
            }

            var eventDto = profiles.Select(profile => profile.ToDto()).ToList();
            return Ok(eventDto);

                
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }
            
    }
}