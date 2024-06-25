using Common.Dtos.Taxonomias;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxonomiasController(ITaxonomias taxonomiasRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VerticalDto>>> GetTaxonomias()
        {
            try
            {
                var taxonomias = await taxonomiasRepository.GetTaxonomias();

                if (taxonomias == null || !taxonomias.Any())
                {
                    return NotFound();
                }

                var taxonomiasDto = taxonomias.Select(v => v.ToDto()).ToList();
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
                var taxonomia = await taxonomiasRepository.GetTaxonomiaById(id);

                if (taxonomia == null)
                {
                    return NotFound();
                }

                var taxonomiaDto = taxonomia.ToDto();
                return Ok(taxonomiaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving data from the database: {ex.Message}");
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
                var existingTaxonomia = await taxonomiasRepository.GetTaxonomiaById(id);

                if (existingTaxonomia == null)
                {
                    return NotFound();
                }

                // Update properties of the existing entity from DTO
                existingTaxonomia.VerticalName = taxonomiaDto.VerticalName;

                // Update related entities (roles and skills)
                UpdateRolesAndSkills(existingTaxonomia, taxonomiaDto.RoleVerticals);
                

                // Update entity in DbContext
                taxonomiasRepository.Update(existingTaxonomia);

                // Save changes to the database
                await taxonomiasRepository.SaveAsync();

                return NoContent(); // 204 No Content response
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Concurrency error occurred while updating the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data in the database: {ex.Message}");
            }
        }

        private void UpdateRolesAndSkills(Vertical existingVertical, List<RoleVerticalDto> roleVerticalDtos)
        {
            // Clear existing roles (if any)
            existingVertical.Roles_verticals.Clear();

            // Add or update roles from DTO
            foreach (var roleDto in roleVerticalDtos)
            {
                var roleEntity = existingVertical.Roles_verticals.FirstOrDefault(r => r.Role_verticalsId == roleDto.RoleVerticalId);
                if (roleEntity == null)
                {
                    roleEntity = new Role_verticals
                    {
                        Role_verticalsId = roleDto.RoleVerticalId,
                        Role_verticalsName = roleDto.RoleVerticalName,
                        VerticalId = existingVertical.VerticalId // Ensure VerticalId is set
                    };
                    existingVertical.Roles_verticals.Add(roleEntity);
                }

                // Update or add skills
                UpdateSkills(roleEntity, roleDto.SkillVerticals);
            }
        }

        private void UpdateSkills(Role_verticals existingRole, List<SkillVerticalDto> skillDtos)
        {
            // Clear existing skills (if any)
            existingRole.Skills_verticals.Clear();

            // Add new skills from DTO
            foreach (var skillDto in skillDtos)
            {
                var skillEntity = new skil_veticals
                {
                    skil_veticalsId = skillDto.SkillVerticalId,
                    skil_veticalsName = skillDto.SkillVerticalName,
                    skil_veticalsExperiencia = skillDto.SkillVerticalExperience,
                    Role_verticalsId = existingRole.Role_verticalsId // Ensure Role_verticalsId is set
                };
                existingRole.Skills_verticals.Add(skillEntity);
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

                var newVertical = new Vertical
                {
                    VerticalId = Guid.NewGuid(),
                    VerticalName = taxonomiaDto.VerticalName
                };

                // Add roles and skills
                if (taxonomiaDto.RoleVerticals != null)
                {
                    foreach (var roleDto in taxonomiaDto.RoleVerticals)
                    {
                        var newRole = new Role_verticals
                        {
                            Role_verticalsId = Guid.NewGuid(),
                            Role_verticalsName = roleDto.RoleVerticalName,
                            VerticalId = newVertical.VerticalId
                        };

                        // Add skills to the role
                        if (roleDto.SkillVerticals != null)
                        {
                            foreach (var skillDto in roleDto.SkillVerticals)
                            {
                                var newSkill = new skil_veticals
                                {
                                    skil_veticalsId = Guid.NewGuid(),
                                    skil_veticalsName = skillDto.SkillVerticalName,
                                    skil_veticalsExperiencia = skillDto.SkillVerticalExperience,
                                    Role_verticalsId = newRole.Role_verticalsId
                                };
                                newRole.Skills_verticals.Add(newSkill);
                            }
                        }

                        newVertical.Roles_verticals.Add(newRole);
                    }
                }

                // Add vertical users
                if (taxonomiaDto.VerticalUsers != null)
                {
                    foreach (var userDto in taxonomiaDto.VerticalUsers)
                    {
                        var newUser = new verticalsUser
                        {
                           UserId = userDto.UserId,
                            VerticalId = newVertical.VerticalId
                        };
                        newVertical.VerticalsUsers.Add(newUser);
                    }
                }

                await taxonomiasRepository.Add(newVertical);
                await taxonomiasRepository.SaveAsync();

                var addedTaxonomiaDto = newVertical.ToDto();
                return CreatedAtAction(nameof(GetTaxonomiaById), new { id = newVertical.VerticalId }, addedTaxonomiaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error creating data in the database: {ex.Message}");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaxonomia(Guid id)
        {
            try
            {
                var taxonomia = await taxonomiasRepository.GetTaxonomiaById(id);

                if (taxonomia == null)
                {
                    return NotFound();
                }

                taxonomiasRepository.Delete(id);
                await taxonomiasRepository.SaveAsync();

                return NoContent(); // 204 No Content response
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting data from the database: {ex.Message}");
            }
        }
    }
}