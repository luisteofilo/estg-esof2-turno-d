namespace ESOF.WebApp.DBLayer.Entities;

public class Timesheet
{
    public int TimesheetId { get; set; }
    public DateTime Date { get; set; }
    public int HoursWorked { get; set; }
    public string TaskDescription { get; set; }
    
    // Foreign key
    public int PositionId { get; set; }
    
    // Navigation property
    public Position Position { get; set; }
}
