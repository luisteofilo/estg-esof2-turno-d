using System.ComponentModel.DataAnnotations;
using Helpers.Job;
namespace ESOF.WebApp.DBLayer.Entities;

public class Timesheet
{
    [Key]
    public Guid TimesheetId { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public int HoursWorked { get; set; }
    public string TaskDescription { get; set; }
    
    public Guid PositionId { get; set; }

    public Position Position { get; set; }
    
    public Guid? InvoiceId { get; set; }

    public Invoice Invoice { get; set; }
}
