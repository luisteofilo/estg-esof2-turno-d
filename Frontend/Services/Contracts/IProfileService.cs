using ESOF.WebApp.WebAPI.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface IProfileService
{
        Task<ProfileDto> GetProfile();
}