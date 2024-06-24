using Common.Dtos.Interview;
using Frontend.Services.Contracts;

namespace Frontend.Services;
public class CandidateService(HttpClient _httpClient) : ICandidateService
{
    public async Task<IEnumerable<CandidateDto>> GetCandidatesAsync()
    {
        var response = await _httpClient.GetAsync("api/Candidates");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<CandidateDto>>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error getting candidates");
        }
    }

    public async Task CreateCandidateAsync(CandidateDto candidate)
    {
        // Isto serve para ver se um Candidateo com o mesmo nome já existe
        var candidates = await GetCandidatesAsync();
        if (candidates.Any(c => string.Equals(c.Name, candidate.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new HttpRequestException($"A candidate with the name {candidate.Name} already exists.");
        }

        var response = await _httpClient.PostAsJsonAsync("api/Candidates", candidate);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error creating candidate");
        }
    }
}