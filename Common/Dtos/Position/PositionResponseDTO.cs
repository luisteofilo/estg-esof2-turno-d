namespace Common.Dtos.Position;

public class PositionResponseDTO
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BillingType { get; set; }
    
    public string? JobDetails { get; set; }
}