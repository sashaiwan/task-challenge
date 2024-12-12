using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskChallenge.Application;
using TaskChallenge.Application.Interfaces;
using TaskChallenge.Application.Services;
using TaskChallenge.Application.Validators;
using TaskChallenge.Infrastructure;
using TaskChallenge.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger stuff
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Angular app URL
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<TaskDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
// Add Service layer
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskValidator>();

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

app.UseCors("AllowAngular");

const string apiBaseUrl = "/api";
// Health check endpoint
app.MapGet($"{apiBaseUrl}/health", async (TaskDbContext dbContext) =>
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

// Tasks endpoints
app.MapGet($"{apiBaseUrl}/tasks", async (bool? completed, ITaskService taskService) => 
{
    var result = await taskService.GetAllAsync(completed);
    
    return result.Match(
        success: Results.Ok,
        failure: error => error.Name switch
        {
            ErrorName.NotFound => Results.NotFound(error.Message),
            _ => Results.StatusCode(500)
        }
    );
});

app.MapPost($"{apiBaseUrl}/tasks", async (
    TaskModel task,
    ITaskService taskService) =>
{
    var result = await taskService.CreateAsync(task);
    
    return result.Match(
        success: Results.Ok,
        failure: error => error.Name switch
        {
            ErrorName.ValidationError => Results.ValidationProblem(error.Messages!),
            _ => Results.StatusCode(500)
        }
    );
});

app.MapPut($"{apiBaseUrl}/tasks/{{id:int}}", async (
    int id,
    TaskModel task,
    ITaskService taskService) =>
{
    var result = await taskService.UpdateAsync(id, task);
    
    return result.Match(
        success: Results.Ok,
        failure: error => error.Name switch
        {
            ErrorName.NotFound => Results.NotFound(error.Message),
            ErrorName.ValidationError => Results.ValidationProblem(error.Messages!),
            _ => Results.StatusCode(500)
        }
    );
});

app.MapDelete($"{apiBaseUrl}/tasks/{{id:int}}", async (int id, ITaskService taskService) =>
{
    var result = await taskService.DeleteAsync(id);
    return result.Match(
        success: _ => Results.Ok(),
        failure: error => error.Name switch
        {
            ErrorName.NotFound => Results.NotFound(error.Message),
            _ => Results.StatusCode(500)
        }
    );
});

app.Run();