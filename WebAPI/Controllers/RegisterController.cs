using System.Text.Json;
using Common.Dtos.Users;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterRepository _registerRepository;

        public RegisterController(RegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _registerRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.UserConvertToDto();
            return Ok(userDto);
        }

        [HttpPost("admin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin(UserDto userDto)
        {
            Console.WriteLine(userDto.Email);
            var user = userDto.DtoConvertToUser();
            Console.WriteLine(user.Email);
            var registeredUser = await _registerRepository.RegisterAdminAsync(user);
            var registeredUserDto = registeredUser.UserConvertToDto();
            return CreatedAtAction(nameof(GetUser), new { id = registeredUserDto.UserId }, registeredUserDto);
        }

        [HttpPost("client")]
        public async Task<ActionResult<ClientDto>> RegisterClient(ClientUserDto clientUserDto)
        {
            Console.WriteLine($"Received Client DTO: {JsonSerializer.Serialize(clientUserDto.ClientDto)}");
            Console.WriteLine($"Received User DTO: {JsonSerializer.Serialize(clientUserDto.UserDto)}");

            var client = clientUserDto.ClientDto.DtoConvertToClient();
            var user = clientUserDto.UserDto.DtoConvertToUser();

            try
            {
                var registeredClient = await _registerRepository.RegisterClientWithUserAsync(client, user);
                if (registeredClient == null)
                {
                    Console.WriteLine("Failed to register client in the repository");
                    return BadRequest("Failed to register client");
                }

                var registeredClientDto = registeredClient.ClientConvertToDto();
                return CreatedAtAction(nameof(GetUser), new { id = registeredClientDto.ClientId }, registeredClientDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return BadRequest($"Failed to register client: {ex.Message}");
            }
        }
        
        [HttpPost("talent")]
        public async Task<ActionResult<TalentDto>> RegisterTalent(TalentUserDto talentUserDto)
        {
            Console.WriteLine($"Received Client DTO: {JsonSerializer.Serialize(talentUserDto.TalentDto)}");
            Console.WriteLine($"Received User DTO: {JsonSerializer.Serialize(talentUserDto.UserDto)}");

            var talent = talentUserDto.TalentDto.DtoConvertToTalent();
            var user = talentUserDto.UserDto.DtoConvertToUser();

            try
            {
                var registeredTalent = await _registerRepository.RegisterTalentWithUserAsync(talent, user);
                if (registeredTalent == null)
                {
                    Console.WriteLine("Failed to register client in the repository");
                    return BadRequest("Failed to register client");
                }

                var registeredTalentDto = registeredTalent.TalentConvertToDto();
                return CreatedAtAction(nameof(GetUser), new { id = registeredTalentDto.TalentId }, registeredTalentDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return BadRequest($"Failed to register client: {ex.Message}");
            }
        }
        
        
    }
}
