using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Position;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.Services;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly PositionService _positionService;

        public PositionsController(PositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionResponseDTO>>> GetAllPositions()
        {
            var positions = await _positionService.GetAllPositions();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionResponseDTO>> GetPositionById(Guid id)
        {
            var position = await _positionService.GetPositionById(id);
            if (position == null)
            {
                return NotFound();
            }
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePosition(Guid jobId, [FromBody] PositionCreateDTO dto)
        {
            await _positionService.CreatePosition(jobId, dto);
            return Ok();
            // return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.PositionId }, createdPosition);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(Guid id, PositionUpdateDTO dto)
        {
            
            await _positionService.UpdatePosition(id, dto);
            return Ok(); 
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(Guid id)
        {
            var result = await _positionService.DeletePosition(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
