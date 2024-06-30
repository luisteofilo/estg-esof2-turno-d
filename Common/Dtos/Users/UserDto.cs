using Common.Dtos.RoleAndPerms;

namespace Common.Dtos.Users;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}