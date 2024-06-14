using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;

namespace ESOF.WebApp.WebAPI.Services;

public class JobFAQService
{
    public static async Task AddQuestion(Guid jobId, string questionText)
    {
        var db = new ApplicationDbContext();
        var question = new Question
        {
            QuestionText = questionText,
        };
        db.FAQQuestions.Add(question);
        await db.SaveChangesAsync();
    }
    
}