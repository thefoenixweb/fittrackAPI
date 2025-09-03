namespace FitTrack.API.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Weight { get; set; }
        public string Height { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
    
    public class CreateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Weight { get; set; }
        public string Height { get; set; } = string.Empty;
    }
    
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Weight { get; set; }
        public string Height { get; set; } = string.Empty;
    }
}
