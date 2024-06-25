using Common.Dtos.FAQ;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Services;

public class JobFAQRepository
{
    private readonly ApplicationDbContext _db = new ApplicationDbContext();

    public async Task<IEnumerable<Question>> GetQuestions(Guid jobId)
    {
        return await _db.FAQQuestions
            .Where(q => q.Job.JobId == jobId)
            .Include(q => q.Answers)
            .ToListAsync();
        
    }
    
    public async Task<IEnumerable<Answer>> GetAnswersForQuestion(Guid questionId)
    {
        return await _db.FAQQuestions
            .Where(q => q.QuestionId == questionId)
            .SelectMany(q => q.Answers)
            .ToListAsync();
    }

    public async Task AddQuestion(Guid jobId, string questionText)
    {
        var question = new Question
        {
            QuestionText = questionText,
        };
        _db.FAQQuestions.Add(question);
        await _db.SaveChangesAsync();
    }
    
    public async Task AnswerQuestion(Guid questionId, User author, string answerText)
    {
        var answer = new Answer
        {
            AnswerText = answerText,
            Author = author,
        };
        _db.FAQAnswers.Add(answer);
        
        var question = await _db.FAQQuestions.FindAsync(questionId);
        question?.Answers.Add(answer);

        await _db.SaveChangesAsync();
    }
    
    public async Task DeleteQuestion(Guid questionId)
    {
        var question = await _db.FAQQuestions.FindAsync(questionId);
        if (question != null)
        {
            _db.FAQQuestions.Remove(question);
            await _db.SaveChangesAsync();
        }
    }
    
    public async Task<List<Question>> SearchQuestions(string query)
    {
        var questions = _db.FAQQuestions.Where(q => 
            q.QuestionText.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
            q.Answers.Any(a => a.AnswerText.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
        return await questions.ToListAsync();
    }
    
}