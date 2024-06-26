using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Dtos.Job;
using Common.Dtos.Position;
using Frontend.Services.Contracts;

namespace Frontend.Services;

public class PositionService(HttpClient _httpClient) : IPositionService
{
    public async Task CreatePosition(Guid jobId, PositionCreateDTO dto)
    {
        var request = new CreatePositionRequest
        {
            JobId = jobId,
            Position = dto
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/Positions", request);
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error Creating Position");
        }
    }
    
    public async Task<IEnumerable<PositionResponseDTO>> GetPositions()
    {
        var response = await _httpClient.GetAsync("api/Positions");
        
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            //return await response.Content.ReadFromJsonAsync<IEnumerable<PositionResponseDTO>>();
            // var positions = JsonSerializer.Deserialize<List<PositionResponseDTO>>(responseString);
            // return positions;
            
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            options.Converters.Add(new JsonStringEnumConverter());
            var oDataResponse = JsonSerializer.Deserialize<List<PositionResponseDTO>>(responseString, options);

            return oDataResponse;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error getting candidates");
        }
    }
}

public class CreatePositionRequest
{
    public Guid JobId { get; set; }
    public PositionCreateDTO Position { get; set; }
}
