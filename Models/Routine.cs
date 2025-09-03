using System.ComponentModel.DataAnnotations;

namespace FitTrack.API.Models
{
    public class Routine
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Difficulty { get; set; } = string.Empty;
        
        public List<Exercise> Exercises { get; set; } = new();
        
        [Range(1, 300)]
        public int EstimatedDuration { get; set; }
        
        [Range(0, 100)]
        public int OverallIntensity { get; set; }
        
        public List<string> Schedule { get; set; } = new();
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsFavorite { get; set; } = false;
        
        // Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
