using FitTrack.API.Data;
using FitTrack.API.DTOs;
using FitTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.API.Services
{
    public class WorkoutHistoryService : IWorkoutHistoryService
    {
        private readonly FitTrackDbContext _context;
        
        public WorkoutHistoryService(FitTrackDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<WorkoutHistoryDto>> GetAllWorkoutHistoryAsync(Guid userId)
        {
            var workoutHistory = await _context.WorkoutHistory
                .Include(w => w.Exercises)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
                
            return workoutHistory.Select(MapToDto);
        }
        
        public async Task<WorkoutHistoryDto?> GetWorkoutHistoryByIdAsync(Guid id, Guid userId)
        {
            var workoutHistory = await _context.WorkoutHistory
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
                
            return workoutHistory != null ? MapToDto(workoutHistory) : null;
        }
        
        public async Task<WorkoutHistoryDto> CreateWorkoutHistoryAsync(CreateWorkoutHistoryDto createWorkoutHistoryDto, Guid userId)
        {
            var workoutHistory = new WorkoutHistory
            {
                RoutineId = createWorkoutHistoryDto.RoutineId,
                RoutineName = createWorkoutHistoryDto.RoutineName,
                Date = createWorkoutHistoryDto.Date,
                Duration = createWorkoutHistoryDto.Duration,
                Intensity = createWorkoutHistoryDto.Intensity,
                UserId = userId,
                Exercises = createWorkoutHistoryDto.Exercises.Select(e => new Exercise
                {
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                    Intensity = e.Intensity
                }).ToList()
            };
            
            _context.WorkoutHistory.Add(workoutHistory);
            await _context.SaveChangesAsync();
            
            return MapToDto(workoutHistory);
        }
        
        public async Task<bool> DeleteWorkoutHistoryAsync(Guid id, Guid userId)
        {
            var workoutHistory = await _context.WorkoutHistory
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
                
            if (workoutHistory == null) return false;
            
            _context.WorkoutHistory.Remove(workoutHistory);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        public async Task<IEnumerable<WorkoutHistoryDto>> GetWorkoutHistoryByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            var workoutHistory = await _context.WorkoutHistory
                .Include(w => w.Exercises)
                .Where(w => w.UserId == userId && w.Date >= startDate && w.Date <= endDate)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
                
            return workoutHistory.Select(MapToDto);
        }
        
        private static WorkoutHistoryDto MapToDto(WorkoutHistory workoutHistory)
        {
            return new WorkoutHistoryDto
            {
                Id = workoutHistory.Id,
                RoutineId = workoutHistory.RoutineId,
                RoutineName = workoutHistory.RoutineName,
                Date = workoutHistory.Date,
                Duration = workoutHistory.Duration,
                Intensity = workoutHistory.Intensity,
                Exercises = workoutHistory.Exercises.Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                    Intensity = e.Intensity
                }).ToList()
            };
        }
    }
}
