namespace Common.Dtos.Timesheet;


public class TimesheetCreateDTO
{
    public DateTime Date { get; set; }
    
    public int HoursWorked { get; set; }
    
    public string TaskDescription { get; set; }
}