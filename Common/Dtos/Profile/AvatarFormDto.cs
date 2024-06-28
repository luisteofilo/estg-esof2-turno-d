using Microsoft.AspNetCore.Http;

namespace Common.Dtos.Profile;

public class AvatarFormDto
{
    public IFormFile File { get; set; }
    public Guid ProfileId { get; set; }
}