using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Services;

public class JobFAQService
{
    private readonly ApplicationDbContext _db;
    
    public JobFAQService(ApplicationDbContext db)
    {
        _db = db;
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
    
    public async Task<List<Question>> SearchQuestions(string query)
    {
        var questions = _db.FAQQuestions.Where(q => 
            q.QuestionText.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
            q.Answers.Any(a => a.AnswerText.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
        return await questions.ToListAsync();
    }
    
}