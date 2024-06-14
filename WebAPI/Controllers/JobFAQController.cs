using ESOF.WebApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobFAQController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    
    [HttpGet("{jobId}/questions")]
    public IActionResult GetJobQuestions(Guid jobId)
    {
        return Ok();
    }
    
    [HttpGet("{jobId}/questions/{questionId}")]
    public IActionResult GetJobQuestion(Guid jobId, Guid questionId)
    {
        return Ok();
    }
    
    [HttpGet("{jobId}/questions/{questionId}/answers")]
    public IActionResult GetJobQuestionAnswers(Guid jobId, Guid questionId)
    {
        return Ok();
    }
    
    [HttpPost("{jobId}/questions")]
    public async Task<ActionResult> PostJobQuestion(Guid jobId, [FromBody] string questionText)
    {
        try
        {
            await JobFAQService.AddQuestion(jobId, questionText);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    
    [HttpDelete("{jobId}/questions/{questionId}")]
    public IActionResult DeleteJobQuestion(Guid jobId, Guid questionId, [FromBody] Guid userId)
    {
        return Ok();
    }
}