using FitTrack.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FitTrack.API.Data
{
    public class FitTrackDbContext : DbContext
    {
        public FitTrackDbContext(DbContextOptions<FitTrackDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutHistory> WorkoutHistory { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Weight).HasPrecision(5, 2);
            });
            
            // Configure Routine entity
            modelBuilder.Entity<Routine>(entity =>
            {
                entity.Property(e => e.Schedule)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                    );
                    
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Routines)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Configure Exercise entity - Fix cascade delete issue
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.Property(e => e.Weight).HasPrecision(6, 2);
                
                // Relationship with Routine - Keep cascade delete
                entity.HasOne(e => e.Routine)
                    .WithMany(r => r.Exercises)
                    .HasForeignKey(e => e.RoutineId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                // Relationship with WorkoutHistory - Change to NO ACTION to avoid cascade conflict
                entity.HasOne(e => e.WorkoutHistory)
                    .WithMany(w => w.Exercises)
                    .HasForeignKey(e => e.WorkoutHistoryId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            
            // Configure WorkoutHistory entity
            modelBuilder.Entity<WorkoutHistory>(entity =>
            {
                entity.HasOne(w => w.User)
                    .WithMany(u => u.WorkoutHistory)
                    .HasForeignKey(w => w.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
