namespace ESOF.WebApp.DBLayer.Entities;

public class Position
{
    public int PositionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BillingType { get; set; } // Could be "weekly", "monthly", "hourly"
    
    // Foreign key
    public int JobId { get; set; }
    
    // Navigation properties
    public Job Job { get; set; }
    public ICollection<Timesheet> Timesheets { get; set; }
}
