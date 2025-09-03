using FitTrack.API.Data;
using FitTrack.API.DTOs;
using FitTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.API.Services
{
    public class RoutineService : IRoutineService
    {
        private readonly FitTrackDbContext _context;
        
        public RoutineService(FitTrackDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<RoutineDto>> GetAllRoutinesAsync(Guid userId)
        {
            var routines = await _context.Routines
                .Include(r => r.Exercises)
                .Where(r => r.UserId == userId)
                .ToListAsync();
                
            return routines.Select(MapToDto);
        }
        
        public async Task<RoutineDto?> GetRoutineByIdAsync(Guid id, Guid userId)
        {
            var routine = await _context.Routines
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
                
            return routine != null ? MapToDto(routine) : null;
        }
        
        public async Task<RoutineDto> CreateRoutineAsync(CreateRoutineDto createRoutineDto, Guid userId)
        {
            var routine = new Routine
            {
                Name = createRoutineDto.Name,
                Category = createRoutineDto.Category,
                Difficulty = createRoutineDto.Difficulty,
                EstimatedDuration = createRoutineDto.EstimatedDuration,
                OverallIntensity = createRoutineDto.OverallIntensity,
                Schedule = createRoutineDto.Schedule,
                IsFavorite = createRoutineDto.IsFavorite,
                UserId = userId,
                Exercises = createRoutineDto.Exercises.Select(e => new Exercise
                {
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                    Intensity = e.Intensity
                }).ToList()
            };
            
            _context.Routines.Add(routine);
            await _context.SaveChangesAsync();
            
            return MapToDto(routine);
        }
        
        public async Task<RoutineDto?> UpdateRoutineAsync(Guid id, UpdateRoutineDto updateRoutineDto, Guid userId)
        {
            var routine = await _context.Routines
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
                
            if (routine == null) return null;
            
            routine.Name = updateRoutineDto.Name;
            routine.Category = updateRoutineDto.Category;
            routine.Difficulty = updateRoutineDto.Difficulty;
            routine.EstimatedDuration = updateRoutineDto.EstimatedDuration;
            routine.OverallIntensity = updateRoutineDto.OverallIntensity;
            routine.Schedule = updateRoutineDto.Schedule;
            routine.IsFavorite = updateRoutineDto.IsFavorite;
            
            // Update exercises
            _context.Exercises.RemoveRange(routine.Exercises);
            routine.Exercises = updateRoutineDto.Exercises.Select(e => new Exercise
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight,
                Intensity = e.Intensity,
                RoutineId = routine.Id
            }).ToList();
            
            await _context.SaveChangesAsync();
            
            return MapToDto(routine);
        }
        
        public async Task<bool> DeleteRoutineAsync(Guid id, Guid userId)
        {
            var routine = await _context.Routines
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
                
            if (routine == null) return false;
            
            _context.Routines.Remove(routine);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        public async Task<bool> ToggleFavoriteAsync(Guid id, Guid userId)
        {
            var routine = await _context.Routines
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
                
            if (routine == null) return false;
            
            routine.IsFavorite = !routine.IsFavorite;
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        private static RoutineDto MapToDto(Routine routine)
        {
            return new RoutineDto
            {
                Id = routine.Id,
                Name = routine.Name,
                Category = routine.Category,
                Difficulty = routine.Difficulty,
                EstimatedDuration = routine.EstimatedDuration,
                OverallIntensity = routine.OverallIntensity,
                Schedule = routine.Schedule,
                CreatedAt = routine.CreatedAt,
                IsFavorite = routine.IsFavorite,
                Exercises = routine.Exercises.Select(e => new ExerciseDto
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
