namespace Common.Dtos.Timesheet;

public class TimesheetDTOConverter
{
    public ESOF.WebApp.DBLayer.Entities.Timesheet TimesheetCreateDtoToTimesheet(TimesheetCreateDTO dto, ESOF.WebApp.DBLayer.Entities.Position position)
    {
        
        return new ESOF.WebApp.DBLayer.Entities.Timesheet()
        {
            Position = position ,
            HoursWorked = dto.HoursWorked,
            TaskDescription = dto.TaskDescription,
            Date = dto.Date
        };
    }
}