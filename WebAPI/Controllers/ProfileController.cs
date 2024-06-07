using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.WebAPI.Dtos.Profile;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        try
        {
            var db = new ApplicationDbContext();

            var profile = await db.Profiles
                .Include(p => p.ProfileSkills)
                .ThenInclude(ps => ps.Skill)
                .Include(p => p.Experiences)
                .Include(p => p.Educations)
                // Hard coded Profile ID
                .FirstOrDefaultAsync(p => p.ProfileId == Guid.Parse("d841ca87-e89d-4eea-9953-95f2d44dcbda"));

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