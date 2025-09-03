using System.ComponentModel.DataAnnotations;

namespace FitTrack.API.Models
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string MuscleGroup { get; set; } = string.Empty;
        
        [Range(1, 20)]
        public int Sets { get; set; }
        
        [Range(1, 100)]
        public int Reps { get; set; }
        
        [Range(0, 1000)]
        public decimal Weight { get; set; }
        
        [Range(0, 100)]
        public int Intensity { get; set; }
        
        // Navigation properties
        public Guid? RoutineId { get; set; }
        public Routine? Routine { get; set; }
        
        public Guid? WorkoutHistoryId { get; set; }
        public WorkoutHistory? WorkoutHistory { get; set; }
    }
}
