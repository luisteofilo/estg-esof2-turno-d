using ESOF.WebApp.DBLayer.Entities;

namespace Frontend.Services.Contracts;

public interface IUserService
{
    public Task<List<User>?> GetUsersByRoleAsync(Guid roleId);
}