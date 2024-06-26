using Common.Dtos.Users;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

public class UserController : ControllerBase
{
    private RoleRepository _roleRepository;
    
    public UserController(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet("{roleId:guid}/users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(Guid roleId)
    {
        var users = await _roleRepository.GetUsersByRoleAsync(roleId);
        var userDtos = users.Select(user => user.UserConvertToDto()).ToList();
        return Ok(userDtos);
    }
    
}