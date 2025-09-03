using FitTrack.API.DTOs;

namespace FitTrack.API.Services
{
    public interface IRoutineService
    {
        Task<IEnumerable<RoutineDto>> GetAllRoutinesAsync(Guid userId);
        Task<RoutineDto?> GetRoutineByIdAsync(Guid id, Guid userId);
        Task<RoutineDto> CreateRoutineAsync(CreateRoutineDto createRoutineDto, Guid userId);
        Task<RoutineDto?> UpdateRoutineAsync(Guid id, UpdateRoutineDto updateRoutineDto, Guid userId);
        Task<bool> DeleteRoutineAsync(Guid id, Guid userId);
        Task<bool> ToggleFavoriteAsync(Guid id, Guid userId);
    }
}
