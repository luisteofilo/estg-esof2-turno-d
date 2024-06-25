using Common.Dtos.RoleAndPerms;
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Users;

public static class DtoConversion
{
    
    public static User DtoConvertToUser(this UserDto userDto)
    {
        return new User
        {
            UserId = userDto.UserId,
            Email = userDto.Email,
            PasswordHash = userDto.PasswordHash,
            PasswordSalt = userDto.PasswordSalt,
        };
    }
    
    public static UserDto UserConvertToDto(this User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
        };
    }
    
    public static Client DtoConvertToClient(this ClientDto clientDto)
    {
        return new Client
        {
            ClientId = clientDto.ClientId,
            CompanyName = clientDto.CompanyName,
            Phone = clientDto.Phone,
            Address = clientDto.Address,
            City = clientDto.City,
            Country = clientDto.Country,
            PostalCode = clientDto.PostalCode,
            CompanyDescription = clientDto.CompanyDescription,
            UserId = clientDto.UserId
        };
    }
    
    public static ClientDto ClientConvertToDto(this Client client)
    {
        return new ClientDto
        {
            ClientId = client.ClientId,
            CompanyName = client.CompanyName,
            Phone = client.Phone,
            Address = client.Address,
            City = client.City,
            Country = client.Country,
            PostalCode = client.PostalCode,
            CompanyDescription = client.CompanyDescription,
            UserId = client.UserId
        };
    }
    
    public static TalentDto TalentConvertToDto(this Talent talent)
    {
        return new TalentDto
        {
            TalentId = talent.TalentId,
            Name = talent.Name,
            Phone = talent.Phone,
            Address = talent.Address,
            City = talent.City,
            Country = talent.Country,
            PostalCode = talent.PostalCode,
            AreaOfInterest = talent.AreaOfInterest,
            UserId = talent.UserId
        };
    }
    
    public static Talent DtoConvertToTalent(this TalentDto talentDto)
    {
        return new Talent
        {
            TalentId = talentDto.TalentId,
            Name = talentDto.Name,
            Phone = talentDto.Phone,
            Address = talentDto.Address,
            City = talentDto.City,
            Country = talentDto.Country,
            PostalCode = talentDto.PostalCode,
            AreaOfInterest = talentDto.AreaOfInterest,
            UserId = talentDto.UserId
        };
    }
    
    
}