using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IProfileRepository
{ 
    Task<Profile> GetProfile();
}