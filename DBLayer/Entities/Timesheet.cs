namespace ESOF.WebApp.DBLayer.Entities;

public class Timesheet
{
    public int TimesheetId { get; set; }
    public DateTime Date { get; set; }
    public int HoursWorked { get; set; }
    public string TaskDescription { get; set; }
    
    public int PositionId { get; set; }

    public Position Position { get; set; }
}
