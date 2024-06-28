using System.Text.Json;
using System.Text.Json.Nodes;
using Common.Dtos.FAQ;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using ESOF.WebApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobFAQController : ControllerBase
{
    private readonly JobFAQRepository _jobFaqRepository;

    public JobFAQController(JobFAQRepository jobFaqRepository)
    {
        _jobFaqRepository = jobFaqRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var jobs = await _jobFaqRepository.GetFaqJobsAsync();
            return Ok(DtoConversion.JobConvertToDto(jobs));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet("{jobId}")]
    public async Task<IActionResult> GetJobTitle(string jobId)
    {
        try
        {
            var jobTitle = await _jobFaqRepository.GetJobTitle(jobId);
            return Ok(jobTitle);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet("{jobId}/questions")]
    public async Task<IActionResult> GetJobQuestions(Guid jobId)
    {
        try
        {
            var questions = await _jobFaqRepository.GetQuestions(jobId);
            var questionDtos = DtoConversion.QuestionConvertToDto(questions);

            return Ok(questionDtos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
    
    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> GetJobQuestion(Guid questionId)
    {
        try
        {
            var question = await _jobFaqRepository.GetQuestion(questionId);
            var questionDto = DtoConversion.QuestionConvertToDto(question);

            return Ok(questionDto);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet("{jobId}/questions/{questionId}")]
    public IActionResult GetJobQuestion(Guid jobId, Guid questionId)
    {
        return Ok();
    }

    [HttpGet("questions/{questionId}/answers")]
    public async Task<ActionResult> GetAnswersForQuestion(Guid questionId)
    {
        try
        {
            var answers = await _jobFaqRepository.GetAnswersForQuestion(questionId);
            var answersDtos = DtoConversion.AnswerConvertToDto(answers);

            return Ok(answersDtos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }


    [HttpPost("{jobId}/questions")]
    public async Task<ActionResult> PostJobQuestion(Guid jobId, [FromBody] QuestionDto question)
    {
        try
        {
            await _jobFaqRepository.AddQuestion(jobId, question.QuestionText);
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

    [HttpPost("questions/{questionId}/answers")]
    public async Task<IActionResult> PostJobQuestionAnswer(Guid jobId, Guid questionId, [FromBody] string answerText)
    {
        await _jobFaqRepository.AnswerQuestion(questionId, null, answerText);
        return Ok();
    }
    
    [HttpGet("{jobId}/questions/search/{query}")]
    public async Task<IActionResult> SearchQuestions(string jobId, string query)
    {
        try
        {
            var questions = await _jobFaqRepository.SearchQuestions(jobId, query);
            var questionDtos = DtoConversion.QuestionConvertToDto(questions);

            return Ok(questionDtos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
    


}
