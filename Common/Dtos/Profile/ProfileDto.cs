namespace Common.Dtos.Profile;

public class ProfileDto
{
    public Guid ProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Location { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
    public string UrlProfile { get; set; }
    public Guid UserId { get; set; }
    
    public ProfileDto Clone()
    {
        return new ProfileDto
        {
            ProfileId = ProfileId,
            FirstName = FirstName,
            LastName = LastName,
            Location = Location,
            Bio = Bio,
            Avatar = Avatar,
            UrlProfile = UrlProfile,
            UserId = UserId
        };
    }
}