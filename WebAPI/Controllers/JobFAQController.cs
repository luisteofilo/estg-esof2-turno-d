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
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{jobId}/questions")]
    public async Task<IActionResult> GetJobQuestions(Guid jobId)
    {
        try
        {
            var questions = await _jobFaqRepository.GetQuestions(jobId);
            Console.Out.WriteLine(questions.Select(q => q.Job.JobId.ToString()));
            var questionDtos = DtoConversion.QuestionConvertToDto(questions);

            return Ok(questionDtos);
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

    [HttpGet("{jobId}/questions/{questionId}/answers")]
    public async Task<ActionResult> GetAnswersForQuestion(Guid jobId, Guid questionId)
    {
        Console.Out.WriteLine($"api/JobFAQ/{jobId}/questions/{questionId}/answers");
        try
        {
            var answers = await _jobFaqRepository.GetAnswersForQuestion(questionId);
            Console.Out.WriteLine(answers.Select(q => q.AnswerText));
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
        Console.Out.WriteLine(question);
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

    [HttpPost("{jobId}/questions/{questionId}/answers")]
    public async Task<IActionResult> PostJobQuestionAnswer(Guid jobId, Guid questionId, [FromBody] string answerText)
    {
        await _jobFaqRepository.AnswerQuestion(questionId, null, answerText);
        return Ok();
    }


}
