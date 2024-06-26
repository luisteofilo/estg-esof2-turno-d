using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Position;
using Common.Dtos.Timesheet;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.Services;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetsController : ControllerBase
    {
        private readonly TimesheetService _timesheetService;

        public TimesheetsController(TimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timesheet>>> GetAllTimesheets()
        {
            var timesheets = await _timesheetService.GetAllTimesheets();
            return Ok(timesheets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Timesheet>> GetTimesheetById(Guid id)
        {
            var timesheet = await _timesheetService.GetTimesheetById(id); 
            
            if (timesheet == null)
            {
                return NotFound();
            }
            return Ok(timesheet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTimesheets(Guid positionId, [FromBody] TimesheetCreateDTO dto)
        {
            await _timesheetService.CreateTimesheets(positionId, dto);
            return Ok();
            // return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.PositionId }, createdPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimesheet(Guid id, TimesheetUpdateDTO dto)
        {
            await _timesheetService.UpdateTimesheet(id, dto);
            return Ok(); 
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimesheet(Guid id)
        {
            var result = await _timesheetService.DeleteTimesheet(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
