using ESOF.WebApp.DBLayer.Entities;

namespace Frontend.Services.Contracts;

public interface IRegisterService
{
    public Task<User?> RegisterAdminAsync(User user);
    
    public Task<Client?> RegisterClientAsync(Client client, User user);

    public Task<Talent?> RegisterTalentAsync(Talent talent, User user);
    
}