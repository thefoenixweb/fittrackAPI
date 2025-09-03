using System.ComponentModel.DataAnnotations;

namespace FitTrack.API.Models
{
    public class WorkoutHistory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid RoutineId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string RoutineName { get; set; } = string.Empty;
        
        public DateTime Date { get; set; }
        
        [Range(1, 300)]
        public int Duration { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Intensity { get; set; } = string.Empty;
        
        public List<Exercise> Exercises { get; set; } = new();
        
        // Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
