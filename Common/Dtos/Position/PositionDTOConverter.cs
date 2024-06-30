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
    
    public PositionResponseDTO PositionToPositionResponseDTO(ESOF.WebApp.DBLayer.Entities.Position position, ESOF.WebApp.DBLayer.Entities.Job? job)
    {
        
        return new PositionResponseDTO
        {
            BillingType = position.BillingType,
            EndDate = position.EndDate,
            StartDate = position.StartDate,
            JobDetails = job != null ? job.OtherDetails : position.Job.OtherDetails
        };
    }
}