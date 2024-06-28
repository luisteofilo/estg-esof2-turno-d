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
        
        [HttpGet("JobSkills")]
        public async Task<ActionResult<IEnumerable<ProfileSkill>>> GetJobSkills()
        {
            var jobSkills = await _dashboardRepository.GetJobSkillsAsync();
            return Ok(jobSkills);
        }
        
        [HttpGet("Experiences")]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
        {
            var experiences = await _dashboardRepository.GetExperiencesAsync();
            return Ok(experiences);
        }
    }
}