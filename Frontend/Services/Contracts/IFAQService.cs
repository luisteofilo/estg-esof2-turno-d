using Common.Dtos.FAQ;
using ESOF.WebApp.DBLayer.Entities.FAQ;

namespace Frontend.Services.Contracts;

public interface IFAQService
{
    Task<IEnumerable<QuestionDto>> GetQuestions(string jobId);
    Task<QuestionDto> GetQuestion(string questionId);
    Task CreateQuestion(string jobId, string question);
    Task<Question> UpdateQuestion(Guid questionId, Question updatedQuestion);
    Task DeleteQuestion(Guid questionId);
    Task<IEnumerable<AnswerDto>> GetAnswersForQuestion(string jobId, string questionId);
    Task CreateAnswerForQuestion(string questionId, string answer);
    Task<Answer> UpdateAnswerForQuestion(Guid questionId, Guid answerId, Answer updatedAnswer);
    Task DeleteAnswerForQuestion(Guid questionId, Guid answerId);
    Task<IEnumerable<Question>> SearchQuestions(string query);
}