namespace Common.Dtos.Position;

public class PositionDTOConverter
{
    public ESOF.WebApp.DBLayer.Entities.Position PositionCreateDTOToPosition(PositionCreateDTO dto, Guid jobId)
    {
        
        return new ESOF.WebApp.DBLayer.Entities.Position
        {
            JobId = jobId, 
            EndDate = dto.EndDate, 
            StartDate = dto.StartDate, 
            BillingType = dto.BillingType,
            
                
        };
    }
}