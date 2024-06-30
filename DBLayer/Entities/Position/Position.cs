using System.ComponentModel.DataAnnotations;
using Helpers.Job;
namespace ESOF.WebApp.DBLayer.Entities;

public class Position
{
    [Key]
    public Guid PositionId { get; set; } = Guid.NewGuid();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BillingType { get; set; } // Pode ser semanal, mensal ou por hora
    
    // chave estrangeira
    public Guid JobId { get; set; }
    
    public Job Job { get; set; }
    public ICollection<Timesheet> Timesheets { get; set; }
}
