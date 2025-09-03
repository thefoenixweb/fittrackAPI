using System.ComponentModel.DataAnnotations;

namespace FitTrack.API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Phone { get; set; }
        
        [Range(50, 500)]
        public decimal Weight { get; set; }
        
        [StringLength(10)]
        public string Height { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public List<Routine> Routines { get; set; } = new();
        public List<WorkoutHistory> WorkoutHistory { get; set; } = new();
        
        // Computed property
        public string FullName => $"{FirstName} {LastName}";
    }
}
