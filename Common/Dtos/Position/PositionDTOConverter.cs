namespace Common.Dtos.Position;

public class PositionDTOConverter
{
    public ESOF.WebApp.DBLayer.Entities.Position PositionCreateDTOToPosition(PositionCreateDTO dto, ESOF.WebApp.DBLayer.Entities.Job job)
    {
        
        return new ESOF.WebApp.DBLayer.Entities.Position
        {
            Job = job ,
            EndDate = dto.EndDate, 
            StartDate = dto.StartDate, 
            BillingType = dto.BillingType,
            
                
        };
    }
}