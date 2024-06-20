namespace ESOF.WebApp.DBLayer.Entities;

public class Job
{
    public int JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation property
    public ICollection<Position> Positions { get; set; }
}
