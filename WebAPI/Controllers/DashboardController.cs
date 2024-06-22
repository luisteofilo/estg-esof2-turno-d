using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("ProfileSkills")]
        public async Task<ActionResult<IEnumerable<ProfileSkill>>> GetProfileSkills()
        {
            var profileSkills = await _dashboardRepository.GetProfileSkillsAsync();
            return Ok(profileSkills);
        }
        
        [HttpGet("Skills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            var skills = await _dashboardRepository.GetListOfSkillsAsync();
            return Ok(skills);
        }
    }
}
