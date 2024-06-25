namespace ESOF.WebApp.DBLayer.Entities;

public class UserCompany
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CompanyId { get; set; }
    public Companies Company { get; set; }
    
    public int YearsWorked { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyEmployed { get; set; }
}