using ESOF.WebApp.DBLayer.Entities.Emails;
using ESOF.WebApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly EmailTemplateService _reademailTemplateService;

        public EmailTemplateController(EmailTemplateService emailTemplateService)
        {
            _reademailTemplateService = emailTemplateService;
        }

        // Endpoint para obter todos os templates da 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailTemplate>>> GetAllTemplates()
        {
            var templates = await _reademailTemplateService.GetAllTemplatesAsync();
            return Ok(templates);
        }

        // Endpoint para obter um template específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailTemplate>> GetTemplateById(int id)
        {
            var template = await _reademailTemplateService.GetTemplateByIdAsync(id);
            
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }

        // Endpoint para adicionar um novo template
        [HttpPost]
        public async Task<ActionResult> AddTemplate([FromBody] EmailTemplate template)
        {
            await _reademailTemplateService.AddTemplateAsync(template);
            return CreatedAtAction(nameof(GetTemplateById), new { id = template.Id }, template);
        }

        // Endpoint para atualizar um template existente
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTemplate(int id, [FromBody] EmailTemplate template)
        {
            if (id != template.Id)
            {
                return BadRequest();
            }
            
            try
            {
            await _reademailTemplateService.UpdateTemplateAsync(template);
            return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update the email template.");
            }
        }

        // Endpoint para deletar um template pelo ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemplate(int id)
        {
            try
            {
            await _reademailTemplateService.DeleteTemplateAsync(id);
            return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete the email template.");
            }
        }
    }
}
