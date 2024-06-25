using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories
{
    public class RegisterRepository
    {
        private readonly ApplicationDbContext _dbContext = new();
        
        public async Task<User> RegisterAdminAsync(User user)
        {
            var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (adminRole == null)
            {
                throw new InvalidOperationException("Admin role does not exist");
            }

            try
            {
                var entityEntry = await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine(entityEntry.Entity.UserId + " " + adminRole.RoleId);
                
                await _dbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = adminRole.RoleId,
                    UserId = entityEntry.Entity.UserId
                });
        
                await _dbContext.SaveChangesAsync();
                return entityEntry.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the admin user", ex);
            }
        }

        public async Task<Client?> RegisterClientWithUserAsync(Client client, User user)
        {
            if (client == null || user == null)
            {
                Console.WriteLine("Client or User is null");
                throw new ArgumentNullException(nameof(client), "Client cannot be null");
            }

            var clientRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Cliente");
            if (clientRole == null)
            {
                Console.WriteLine("Client role does not exist");
                throw new InvalidOperationException("Client role does not exist");
            }

            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = clientRole.RoleId,
                    UserId = user.UserId
                });
                client.UserId = user.UserId;
                client.User = user;

                var entityEntry = await _dbContext.Clients.AddAsync(client);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Client registered successfully: {client.ClientId}");
                return entityEntry.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while registering the client: {ex.Message}");
                throw new Exception("An error occurred while registering the client", ex);
            }
        }

        public async Task<Talent?> RegisterTalentWithUserAsync(Talent talent, User user)
        {
            if (talent == null || user == null)
            {
                Console.WriteLine("Talent or User is null");
                throw new ArgumentNullException(nameof(talent), "Talent cannot be null");
            }

            var talentRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Talento");
            if (talentRole == null)
            {
                Console.WriteLine("Talent role does not exist");
                throw new InvalidOperationException("Talent role does not exist");
            }

            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = talentRole.RoleId,
                    UserId = user.UserId
                });
                talent.UserId = user.UserId;
                talent.User = user;

                var entityEntry = await _dbContext.Talents.AddAsync(talent);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Talent registered successfully: {talent.TalentId}");
                return entityEntry.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while registering the talent: {ex.Message}");
                throw new Exception("An error occurred while registering the talent", ex);
            }
        }
        
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
