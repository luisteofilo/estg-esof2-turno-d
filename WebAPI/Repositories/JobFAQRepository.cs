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
    
    public async Task<Question> GetQuestion(Guid questionId)
    {
        return await _db.FAQQuestions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);
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
        var Job = await _db.Jobs.FindAsync(jobId);
        var question = new Question
        {
            QuestionText = questionText,
            Job = Job
        };
        _db.FAQQuestions.Add(question);
        await _db.SaveChangesAsync();
    }
    
    public async Task AnswerQuestion(Guid questionId, User author, string answerText)
    {
        // USING ROOT FOR NOW
        User? _author = await _db.Users.FindAsync(new Guid("d78ae1d8-ac3f-4b37-83d6-bb51d725fd7b"));
        var question = await _db.FAQQuestions.FindAsync(questionId);

        var answer = new Answer
        {
            AnswerText = answerText,
            Author = _author,
            Question = question
        };
        
        _db.FAQAnswers.Add(answer);

        if (question != null)
        {
            if (question.Answers == null)
            {
                question.Answers = new List<Answer>();
            }
        }

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