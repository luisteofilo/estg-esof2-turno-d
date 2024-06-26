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
}