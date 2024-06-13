namespace ESOF.WebApp.DBLayer.Entities;

public class verticalsUser
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid VerticalId { get; set; }
    public Vertical Vertical { get; set; }
}