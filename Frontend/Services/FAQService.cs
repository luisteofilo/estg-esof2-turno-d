using System.Net.Http.Json;
using Common.Dtos.FAQ;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class FAQService : IFAQService
{
    private readonly HttpClient _httpClient;

    public FAQService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestions(string jobId)
    {
        var response = await _httpClient.GetAsync($"api/JobFAQ/{jobId}/questions");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<QuestionDto>>();
    }

    public async Task<QuestionDto> GetQuestion(string questionId)
    {
        var response = await _httpClient.GetAsync($"api/JobFAQ/question/{questionId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<QuestionDto>();
    }

    public async Task CreateQuestion(string jobId, string question)
    {
        var content = new QuestionDto
        {
            QuestionText = question,
            JobId = new Guid(jobId),
            Answers = new List<AnswerDto>()
        };

        var response = await _httpClient.PostAsJsonAsync($"api/JobFAQ/{jobId}/questions", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Question> UpdateQuestion(Guid questionId, Question updatedQuestion)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/JobFAQ/questions/{questionId}", updatedQuestion);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Question>();
    }

    public async Task DeleteQuestion(Guid questionId)
    {
        var response = await _httpClient.DeleteAsync($"api/JobFAQ/questions/{questionId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<AnswerDto>> GetAnswersForQuestion(string jobId, string questionId)
    {
        var response = await _httpClient.GetAsync($"api/JobFAQ/{jobId}/questions/{questionId}/answers");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<AnswerDto>>();
    }

    public async Task CreateAnswerForQuestion(string questionId, string answer)
    {
        var content = new AnswerDto
        {
            AnswerText = answer,
            QuestionId = new Guid(questionId)
        };

        var response = await _httpClient.PostAsJsonAsync($"api/JobFAQ/questions/{questionId}/answers", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Answer> UpdateAnswerForQuestion(Guid questionId, Guid answerId, Answer updatedAnswer)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/JobFAQ/questions/{questionId}/answers/{answerId}", updatedAnswer);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Answer>();
    }

    public async Task DeleteAnswerForQuestion(Guid questionId, Guid answerId)
    {
        var response = await _httpClient.DeleteAsync($"api/JobFAQ/questions/{questionId}/answers/{answerId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<QuestionDto>> SearchQuestions(string query)
    {
        var response = await _httpClient.GetAsync($"api/JobFAQ/questions/search?query={query}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<QuestionDto>>();
    }

    public async Task<IEnumerable<FaqJobs>> GetFaqJobsAsync()
    {
        var response = await _httpClient.GetAsync("api/JobFAQ");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<FaqJobs>>();
    }
}
