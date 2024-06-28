using System.Text.Json;
using Common.Dtos.Profile;
using Common.Dtos.Users;
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
            if (client == null)
                throw new ArgumentNullException(nameof(client), "Client cannot be null");
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var clientRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Cliente");
            if (clientRole == null)
                throw new InvalidOperationException("Client role does not exist");

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
            catch (DbUpdateException dbEx)
            {
                
                Console.WriteLine($"Database update exception occurred while registering the client: {dbEx.Message}");
                throw new InvalidOperationException("An error occurred while updating the database", dbEx);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Exception occurred while registering the client: {ex.Message}");
                throw;
            }
        }


        public async Task<Talent?> RegisterTalentWithUserAsync(Talent talent, User user)
{
    if (talent == null)
        throw new ArgumentNullException(nameof(talent), "Talent cannot be null");
    if (user == null)
        throw new ArgumentNullException(nameof(user), "User cannot be null");

    var talentRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Talento");
    if (talentRole == null)
        throw new InvalidOperationException("Talent role does not exist");

    try
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.UserRoles.AddAsync(new UserRole
        {
            RoleId = talentRole.RoleId,
            UserId = user.UserId
        });
    }
    catch (DbUpdateException dbEx)
    {
        
        throw new InvalidOperationException("An error occurred while registering the user role", dbEx);
    }

    string[] nameParts = talent.Name.Split(" ");
    if (nameParts.Length < 2)
    {
        throw new InvalidOperationException("Talent name must contain at least a first name and a last name");
    }

    var profile = new Profile
    {
        ProfileId = Guid.NewGuid(),
        Talent = talent,
        User = user,
        UserId = user.UserId,
        FirstName = nameParts[0],
        LastName = nameParts[1],
        Bio = "placeholder",
        Avatar = "placeholder",
        Location = $"{talent.Address}, {talent.PostalCode}, {talent.City}, {talent.Country}",
        UrlProfile = "placeholder",
        ProfileSkills = new List<ProfileSkill>(),
        Educations = new List<Education>(),
        Experiences = new List<Experience>()
    };

    var profileDto = profile.ProfileConvertToDto();
    
    Console.WriteLine(profile.ProfileId);
    Console.WriteLine(JsonSerializer.Serialize(profileDto, new JsonSerializerOptions { WriteIndented = true }));

    talent.UserId = user.UserId;
    talent.User = user;
    talent.Profile = profile;
    talent.ProfileId = profile.ProfileId;

    var talentDto = talent.TalentConvertToDto();
    
    Console.WriteLine(talent.TalentId);
    Console.WriteLine(JsonSerializer.Serialize(talentDto, new JsonSerializerOptions { WriteIndented = true }));

    using (var transaction = await _dbContext.Database.BeginTransactionAsync())
    {
        try
        {
            await _dbContext.Profiles.AddAsync(profile);
            Console.WriteLine($"Profile registered successfully: {profile.ProfileId}");

            var entityEntry = await _dbContext.Talents.AddAsync(talent);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            Console.WriteLine($"Talent registered successfully: {talent.TalentId}");
            return entityEntry.Entity;
        }
        catch (DbUpdateException dbEx)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("An error occurred while saving the talent and profile", dbEx);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("An unexpected error occurred", ex);
        }
    }
}

        
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
