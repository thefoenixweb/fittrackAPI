using FitTrack.API.DTOs;
using FitTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutinesController : ControllerBase
    {
        private readonly IRoutineService _routineService;
        
        public RoutinesController(IRoutineService routineService)
        {
            _routineService = routineService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoutineDto>>> GetRoutines([FromQuery] Guid userId)
        {
            var routines = await _routineService.GetAllRoutinesAsync(userId);
            return Ok(routines);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<RoutineDto>> GetRoutine(Guid id, [FromQuery] Guid userId)
        {
            var routine = await _routineService.GetRoutineByIdAsync(id, userId);
            if (routine == null)
            {
                return NotFound();
            }
            return Ok(routine);
        }
        
        [HttpPost]
        public async Task<ActionResult<RoutineDto>> CreateRoutine([FromBody] CreateRoutineDto createRoutineDto, [FromQuery] Guid userId)
        {
            var routine = await _routineService.CreateRoutineAsync(createRoutineDto, userId);
            return CreatedAtAction(nameof(GetRoutine), new { id = routine.Id, userId }, routine);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<RoutineDto>> UpdateRoutine(Guid id, [FromBody] UpdateRoutineDto updateRoutineDto, [FromQuery] Guid userId)
        {
            var routine = await _routineService.UpdateRoutineAsync(id, updateRoutineDto, userId);
            if (routine == null)
            {
                return NotFound();
            }
            return Ok(routine);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutine(Guid id, [FromQuery] Guid userId)
        {
            var result = await _routineService.DeleteRoutineAsync(id, userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpPatch("{id}/favorite")]
        public async Task<IActionResult> ToggleFavorite(Guid id, [FromQuery] Guid userId)
        {
            var result = await _routineService.ToggleFavoriteAsync(id, userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
