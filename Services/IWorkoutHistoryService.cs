using FitTrack.API.DTOs;

namespace FitTrack.API.Services
{
    public interface IWorkoutHistoryService
    {
        Task<IEnumerable<WorkoutHistoryDto>> GetAllWorkoutHistoryAsync(Guid userId);
        Task<WorkoutHistoryDto?> GetWorkoutHistoryByIdAsync(Guid id, Guid userId);
        Task<WorkoutHistoryDto> CreateWorkoutHistoryAsync(CreateWorkoutHistoryDto createWorkoutHistoryDto, Guid userId);
        Task<bool> DeleteWorkoutHistoryAsync(Guid id, Guid userId);
        Task<IEnumerable<WorkoutHistoryDto>> GetWorkoutHistoryByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}
