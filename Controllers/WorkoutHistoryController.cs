using FitTrack.API.DTOs;
using FitTrack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutHistoryController : ControllerBase
    {
        private readonly IWorkoutHistoryService _workoutHistoryService;
        
        public WorkoutHistoryController(IWorkoutHistoryService workoutHistoryService)
        {
            _workoutHistoryService = workoutHistoryService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutHistoryDto>>> GetWorkoutHistory([FromQuery] Guid userId)
        {
            var workoutHistory = await _workoutHistoryService.GetAllWorkoutHistoryAsync(userId);
            return Ok(workoutHistory);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutHistoryDto>> GetWorkoutHistory(Guid id, [FromQuery] Guid userId)
        {
            var workoutHistory = await _workoutHistoryService.GetWorkoutHistoryByIdAsync(id, userId);
            if (workoutHistory == null)
            {
                return NotFound();
            }
            return Ok(workoutHistory);
        }
        
        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<WorkoutHistoryDto>>> GetWorkoutHistoryByDateRange(
            [FromQuery] Guid userId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var workoutHistory = await _workoutHistoryService.GetWorkoutHistoryByDateRangeAsync(userId, startDate, endDate);
            return Ok(workoutHistory);
        }
        
        [HttpPost]
        public async Task<ActionResult<WorkoutHistoryDto>> CreateWorkoutHistory([FromBody] CreateWorkoutHistoryDto createWorkoutHistoryDto, [FromQuery] Guid userId)
        {
            var workoutHistory = await _workoutHistoryService.CreateWorkoutHistoryAsync(createWorkoutHistoryDto, userId);
            return CreatedAtAction(nameof(GetWorkoutHistory), new { id = workoutHistory.Id, userId }, workoutHistory);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutHistory(Guid id, [FromQuery] Guid userId)
        {
            var result = await _workoutHistoryService.DeleteWorkoutHistoryAsync(id, userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
