namespace ESOF.WebApp.DBLayer.Entities;

public class UserCompany
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CompanyId { get; set; }
    public Companies Company { get; set; }
}