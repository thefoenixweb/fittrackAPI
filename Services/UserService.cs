using FitTrack.API.Data;
using FitTrack.API.DTOs;
using FitTrack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.API.Services
{
    public class UserService : IUserService
    {
        private readonly FitTrackDbContext _context;
        
        public UserService(FitTrackDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToDto);
        }
        
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user != null ? MapToDto(user) : null;
        }
        
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? MapToDto(user) : null;
        }
        
        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Phone = createUserDto.Phone,
                Weight = createUserDto.Weight,
                Height = createUserDto.Height
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return MapToDto(user);
        }
        
        public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.Phone = updateUserDto.Phone;
            user.Weight = updateUserDto.Weight;
            user.Height = updateUserDto.Height;
            
            await _context.SaveChangesAsync();
            
            return MapToDto(user);
        }
        
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Weight = user.Weight,
                Height = user.Height,
                CreatedAt = user.CreatedAt,
                FullName = user.FullName
            };
        }
    }
}
