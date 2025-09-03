namespace FitTrack.API.DTOs
{
    public class WorkoutHistoryDto
    {
        public Guid Id { get; set; }
        public Guid RoutineId { get; set; }
        public string RoutineName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Intensity { get; set; } = string.Empty;
        public List<ExerciseDto> Exercises { get; set; } = new();
    }
    
    public class CreateWorkoutHistoryDto
    {
        public Guid RoutineId { get; set; }
        public string RoutineName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Intensity { get; set; } = string.Empty;
        public List<CreateExerciseDto> Exercises { get; set; } = new();
    }
}
