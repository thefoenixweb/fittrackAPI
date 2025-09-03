using FitTrack.API.Data;
using FitTrack.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<FitTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoutineService, RoutineService>();
builder.Services.AddScoped<IWorkoutHistoryService, WorkoutHistoryService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            // policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
             policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

// Delete existing database and recreate to fix schema issues
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FitTrackDbContext>();
    
    // Delete the database if it exists (to fix the constraint issue)
    context.Database.EnsureDeleted();
    
    // Create the database with the corrected schema
    context.Database.EnsureCreated();
    
    Console.WriteLine("Database recreated successfully with fixed schema!");
}

app.Run();
