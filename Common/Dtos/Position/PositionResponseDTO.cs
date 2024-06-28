using System.Text.Json.Serialization;

namespace Common.Dtos.Position;

public class PositionResponseDTO
{
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }
    
    [JsonPropertyName("billingType")]
    public string BillingType { get; set; }
    
    [JsonPropertyName("jobDetails")]
    public string? JobDetails { get; set; }
}