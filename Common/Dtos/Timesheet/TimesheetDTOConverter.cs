namespace Common.Dtos.Timesheet;

public class TimesheetDTOConverter
{
    public ESOF.WebApp.DBLayer.Entities.Timesheet TimesheetCreateDtoToTimesheet(TimesheetDTO dto, Guid positionId)
    {
        
        return new ESOF.WebApp.DBLayer.Entities.Timesheet()
        {
            PositionId = positionId,
            HoursWorked = dto.HoursWorked,
            TaskDescription = dto.TaskDescription,
            Date = dto.Date,
            InvoiceId = dto.InvoiceId
        };
    }
    
    public TimesheetResponseDTO TimesheetToTimesheetResponseDTO(ESOF.WebApp.DBLayer.Entities.Timesheet timesheet)
    {
        
        return new TimesheetResponseDTO
        {
            HoursWorked = timesheet.HoursWorked,
            TaskDescription = timesheet.TaskDescription,
            Date = timesheet.Date
        };
    }
}