namespace FitTrack.API.DTOs
{
    public class RoutineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public List<ExerciseDto> Exercises { get; set; } = new();
        public int EstimatedDuration { get; set; }
        public int OverallIntensity { get; set; }
        public List<string> Schedule { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public bool IsFavorite { get; set; }
    }
    
    public class CreateRoutineDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public List<CreateExerciseDto> Exercises { get; set; } = new();
        public int EstimatedDuration { get; set; }
        public int OverallIntensity { get; set; }
        public List<string> Schedule { get; set; } = new();
        public bool IsFavorite { get; set; }
    }
    
    public class UpdateRoutineDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public List<ExerciseDto> Exercises { get; set; } = new();
        public int EstimatedDuration { get; set; }
        public int OverallIntensity { get; set; }
        public List<string> Schedule { get; set; } = new();
        public bool IsFavorite { get; set; }
    }
}
