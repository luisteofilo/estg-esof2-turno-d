using ESOF.WebApp.DBLayer.Entities;
using Common.Dtos.Users;
using Frontend.Services.Contracts;

namespace Frontend.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User?> RegisterAdminAsync(User user)
        {
            var userDto = user.UserConvertToDto();
            Console.WriteLine(userDto.Email);
            var response = await _httpClient.PostAsJsonAsync("api/register/admin", userDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<Client?> RegisterClientAsync(Client client, User user)
        {
            var clientUserDto = new ClientUserDto
            {
                ClientDto = client.ClientConvertToDto(),
                UserDto = user.UserConvertToDto()
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/register/client", clientUserDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Client>();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new InvalidOperationException("Email already exists.");
                }
                throw;
            }
        }

        public async Task<Talent?> RegisterTalentAsync(Talent talent, User user)
        {
            var talentUserDto = new TalentUserDto
            {
                TalentDto = talent.TalentConvertToDto(),
                UserDto = user.UserConvertToDto()
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/register/talent", talentUserDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Talent>();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new InvalidOperationException("Email already exists.");
                }
                throw;
            }
        }
    }
}