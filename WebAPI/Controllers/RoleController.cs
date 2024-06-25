using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Common.Dtos.RoleAndPerms;
using Common.Dtos.Users;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RolesController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _roleRepository.GetRolesAsync();
            var roleDtos = roles.Select(role => role.RoleConvertToDto()).ToList();
            return Ok(roleDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoleDto>> GetRole(Guid id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleDto = role.RoleConvertToDto();
            return Ok(roleDto);
        }
        
        [HttpGet("{id:guid}/permissions")]
        public async Task<ActionResult<List<PermissionDto>?>> GetRolePermissions(Guid id)
        {
            var permissions = await _roleRepository.GetRolePermissionsAsync(id);

            if (permissions == null) return NotFound();
            
            var permissionsDto = permissions.Select(permission => permission.PermissionConvertToDto()).ToList();
            return Ok(permissionsDto);

        }

        
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole(RoleDto roleDto)
        {
            var role = roleDto.DtoConvertToRole();
            var createdRole = await _roleRepository.CreateRoleAsync(role);
            var createdRoleDto = createdRole.RoleConvertToDto();
            return CreatedAtAction(nameof(GetRole), new { id = createdRoleDto.RoleId }, createdRoleDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleDto roleDto)
        {
            if (id != roleDto.RoleId)
            {
                return BadRequest();
            }

            var existingRole = await _roleRepository.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound();
            }
            
            existingRole.Name = roleDto.Name;
            
            await _roleRepository.UpdateRoleAsync(existingRole);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleRepository.DeleteRoleAsync(id);
            return NoContent();
        }
        
        [HttpPost("{roleId:guid}/permissions")]
        public async Task<IActionResult> AddPermissionToRole(Guid roleId, PermissionDto permissionDto)
        {
            var permission = permissionDto.DtoConvertToPermission();
            var createdPermission = await _roleRepository.AddPermissionToRoleAsync(roleId, permission);
            var createdPermissionDto = createdPermission.PermissionConvertToDto();
            return CreatedAtAction(nameof(GetRolePermissions), new { id = roleId }, createdPermissionDto);
        }
        
        [HttpDelete("{roleId:guid}/permissions/{permissionId:guid}")]
        public async Task<IActionResult> RemovePermissionFromRole(Guid roleId, Guid permissionId)
        {
            try
            {
                await _roleRepository.RemovePermissionFromRoleAsync(roleId, permissionId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("{roleId:guid}/users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(Guid roleId)
        {
            var users = await _roleRepository.GetUsersByRoleAsync(roleId);
            var userDtos = users.Select(user => user.UserConvertToDto()).ToList();
            return Ok(userDtos);
        }
    }
}
