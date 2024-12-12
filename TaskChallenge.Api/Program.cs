using Microsoft.EntityFrameworkCore;
using TaskChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Swagger stuff
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();

var app = builder.Build();

// Apply migrations on app startup
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
db.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

// Health check endpoint
app.MapGet("/health", async (TaskDbContext dbContext) =>
{
    try
    {
        await dbContext.Database.CanConnectAsync();
        return Results.Ok(new { Status = "Healthy", Message = "Database is connected" });
    }
    catch (Exception ex)
    {
        return Results.Json(
            new { Status = "Unhealthy", Message = "Database connection failed", Error = ex.Message },
            statusCode: 503
        );
    }
});

app.Run();