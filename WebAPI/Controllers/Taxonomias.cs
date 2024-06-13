
using Common.Dtos.Taxonomias;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Taxonomias(ITaxonomias TaxonomiasRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<verticalDto>> GetTaxonomias()
    {
        try
        {
            var Taxonomias = await TaxonomiasRepository.GetTaxonomias();
            
            if(Taxonomias == null)
            {
                return NotFound();
            }

            var eventDto = Taxonomias.ToDto();
            return Ok(eventDto);
                
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database.");
        }
            
    }
}

