namespace ESOF.WebApp.DBLayer.Entities;

public class Position
{
    public int PositionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BillingType { get; set; } // Pode ser semanal, mensal ou por hora
    
    // chave estrangeira
    public int JobId { get; set; }
    
    public Job Job { get; set; }
    public ICollection<Timesheet> Timesheets { get; set; }
}
