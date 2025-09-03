namespace FitTrack.API.DTOs
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int Intensity { get; set; }
    }
    
    public class CreateExerciseDto
    {
        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int Intensity { get; set; }
    }
}
