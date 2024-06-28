using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Dtos.Job;
using Common.Dtos.Position;
using Frontend.Services.Contracts;
using System.Net.Http.Json;

namespace Frontend.Services;

public class PositionService : IPositionService
{
    private readonly HttpClient _httpClient;
    public PositionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
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
            try
            {
                var json = await response.Content.ReadAsStringAsync();
            
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    var root = document.RootElement;
                    if (root.TryGetProperty("$values", out JsonElement positionsJson))
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            AllowTrailingCommas = true,
                            NumberHandling = JsonNumberHandling.AllowReadingFromString,
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var positions = JsonSerializer.Deserialize<List<PositionResponseDTO>>(positionsJson.GetRawText(), options);
                        return positions;
                    }
                    else
                    {
                        throw new JsonException("Invalid JSON structure: $values property not found.");
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                throw;
            }
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error getting positions: {errorMessage}");
        }
    }
}

public class CreatePositionRequest
{
    public Guid JobId { get; set; }
    public PositionCreateDTO Position { get; set; }
}
