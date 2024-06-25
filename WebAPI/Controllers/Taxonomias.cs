using Common.Dtos.Taxonomias;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxonomiasController : ControllerBase
    {
        private readonly ITaxonomias _taxonomiasRepository;

        public TaxonomiasController(ITaxonomias taxonomiasRepository)
        {
            _taxonomiasRepository = taxonomiasRepository;
        }

        [HttpGet]
        public async Task<ActionResult<VerticalDto>> GetTaxonomias()
        {
            try
            {
                var taxonomias = await _taxonomiasRepository.GetTaxonomias();

                if (taxonomias == null)
                {
                    return NotFound();
                }

                var taxonomiasDto = taxonomias.ToDto();
                return Ok(taxonomiasDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VerticalDto>> GetTaxonomiaById(Guid id)
        {
            try
            {
                var taxonomia = await _taxonomiasRepository.GetTaxonomiaById(id);

                if (taxonomia == null)
                {
                    return NotFound();
                }

                var taxonomiaDto = taxonomia.ToDto();
                return Ok(taxonomiaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTaxonomia(Guid id, VerticalDto taxonomiaDto)
        {
            if (id != taxonomiaDto.VerticalId)
            {
                return BadRequest("VerticalId in URL does not match VerticalId in request body.");
            }

            try
            {
               
                var existingTaxonomia = await _taxonomiasRepository.GetTaxonomiaById(id);

                if (existingTaxonomia == null)
                {
                    return NotFound();
                }
                
                existingTaxonomia.VerticalName = taxonomiaDto.VerticalName;
                
                _taxonomiasRepository.Update(existingTaxonomia);

              
                await _taxonomiasRepository.SaveAsync();

                return NoContent(); 
            }
            
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Concurrency error occurred while updating the database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data in the database.");
            }
        }
      [HttpPost]
        public async Task<ActionResult<VerticalDto>> CreateTaxonomia(VerticalDto taxonomiaDto)
        {
            try
            {
                if (taxonomiaDto == null)
                {
                    return BadRequest("Taxonomia data is null.");
                }

                var newTaxonomia = new Vertical
                {
                    VerticalId = Guid.NewGuid(),
                    VerticalName = taxonomiaDto.VerticalName,
                    Roles_verticals = taxonomiaDto.RoleVerticals?.Select(roleDto => new Role_verticals
                    {
                        Role_verticalsId = Guid.NewGuid(),
                        Role_verticalsName = roleDto.RoleVerticalName,
                        Skills_verticals = roleDto.SkillVerticals?.Select(skillDto => new skil_veticals
                        {
                            skil_veticalsId = Guid.NewGuid(),
                            skil_veticalsName = skillDto.SkillVerticalName,
                            skil_veticalsExperiencia = skillDto.SkillVerticalExperience
                        }).ToList()
                    }).ToList(),
                    VerticalsUsers = taxonomiaDto.VerticalUsers?.Select(userDto => new verticalsUser
                    {
                        UserId = userDto.UserId,
                        // Assuming you need to map the User entity here
                    }).ToList()
                };

                // Call repository method to add the new entity
                await _taxonomiasRepository.Add(newTaxonomia);
                await _taxonomiasRepository.SaveAsync();

                // Map the added entity back to DTO and return
                var addedTaxonomiaDto = newTaxonomia.ToDto();
                return CreatedAtAction(nameof(GetTaxonomiaById), new { id = newTaxonomia.VerticalId }, addedTaxonomiaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating data in the database: " + ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaxonomia(Guid id)
        {
            try
            {
                var taxonomia = await _taxonomiasRepository.GetTaxonomiaById(id);

                if (taxonomia == null)
                {
                    return NotFound();
                }

                _taxonomiasRepository.Delete(id);
                await _taxonomiasRepository.SaveAsync();

                return NoContent(); // 204 No Content response
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data from the database: " + ex.Message);
            }
        }
    }
}