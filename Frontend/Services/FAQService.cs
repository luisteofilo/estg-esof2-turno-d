using Common.Dtos.FAQ;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Frontend.Helpers;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class FAQService(HttpClient _httpClient) : IFAQService
{
    public async Task<IEnumerable<QuestionDto>> GetQuestions(string jobId)
    {
        var response = await _httpClient.GetAsync($"api/JobFAQ/{jobId}/questions");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<QuestionDto>>();
    }

    public Task<Question> GetQuestion(Guid questionId)
    {
        throw new NotImplementedException();
    }

    public Task<Question> CreateQuestion(Question question)
    {
        throw new NotImplementedException();
    }

    public Task<Question> UpdateQuestion(Guid questionId, Question updatedQuestion)
    {
        throw new NotImplementedException();
    }

    public Task DeleteQuestion(Guid questionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AnswerDto>> GetAnswersForQuestion(string jobId, string questionId)
    {
        Console.Out.WriteLine($"api/JobFAQ/{jobId}/questions/{questionId}/answers");
        var response = await _httpClient.GetAsync($"api/JobFAQ/{jobId}/questions/{questionId}/answers");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<AnswerDto>>();
    }

    public Task<Answer> CreateAnswerForQuestion(Guid questionId, Answer answer)
    {
        throw new NotImplementedException();
    }

    public Task<Answer> UpdateAnswerForQuestion(Guid questionId, Guid answerId, Answer updatedAnswer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAnswerForQuestion(Guid questionId, Guid answerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Question>> SearchQuestions(string query)
    {
        throw new NotImplementedException();
    }
}