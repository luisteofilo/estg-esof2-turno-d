
namespace Common.Dtos.Profile;
public class ProfileDto
{
    public Guid ProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
    public string Location { get; set; }
    public string UrlBackground { get; set; }
    public string UrlProfile { get; set; }
    public Guid UserId { get; set; }
}