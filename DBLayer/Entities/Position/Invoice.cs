using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Invoice
{
    [Key]
    public Guid InvoiceId { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public int Payment { get; set; }
    public string Description { get; set; }
    
    public Guid TimesheetId { get; set; }

    public Timesheet Timesheet { get; set; }
}