using System.Collections.Generic;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.Services;
using ESOF.WebApp.WebAPI.Services;
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
        public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
            var positions = await _positionService.GetAllPositions();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPositionById(int id)
        {
            var position = await _positionService.GetPositionById(id);
            if (position == null)
            {
                return NotFound();
            }
            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<Position>> CreatePosition(Position position)
        {
            var createdPosition = await _positionService.CreatePosition(position);
            return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.PositionId }, createdPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, Position position)
        {
            if (id != position.PositionId)
            {
                return BadRequest();
            }

            var updatedPosition = await _positionService.UpdatePosition(position);
            return Ok(updatedPosition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
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
