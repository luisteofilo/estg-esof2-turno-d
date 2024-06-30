namespace Common.Dtos.Timesheet;


public class TimesheetDTO
{
    public DateTime Date { get; set; }
    
    public int HoursWorked { get; set; }
    
    public string TaskDescription { get; set; }
    
    public Guid? InvoiceId { get; set; }
}