namespace Common.Dtos.Position;

public static class PositionDTOConverter
{
    public static ESOF.WebApp.DBLayer.Entities.Position PositionCreateDTOToPosition(this PositionCreateDTO dto)
    {
        
        if (dto.EndDate.Value != null) 
        {
            DateTime endDateWithKind = DateTime.SpecifyKind(dto.EndDate.Value, DateTimeKind.Utc);
            dto.EndDate = endDateWithKind;
        }
        
        return new ESOF.WebApp.DBLayer.Entities.Position
        {
            ClientId = jobDto.ClientId,
            EndDate = jobDto.EndDate,
            Positions = jobDto.Positions,
            Commitment = jobDto.Commitment,
            Remote = jobDto.Remote,
            Localization = jobDto.Localization,
            Education = jobDto.Education,
            Experience = jobDto.Experience,
            Description = jobDto.Description
        };
    }
}