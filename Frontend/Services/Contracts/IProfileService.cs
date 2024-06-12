

namespace Frontend.Services.Contracts;

public interface IProfileService
{
        Task<ProfileDto> GetProfile();
}