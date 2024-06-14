using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewRepository _interviewRepository;

        public InterviewsController(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Interview>>> GetInterviews()
        {
            var interviews = await _interviewRepository.GetAllInterviews();
            return Ok(interviews);
        }
    }
}