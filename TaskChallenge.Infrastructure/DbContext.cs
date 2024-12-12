using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskChallenge.Infrastructure.Models;

namespace TaskChallenge.Infrastructure;

public class TaskDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(255);
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
        
        SeedData(modelBuilder.Entity<TaskModel>());
    }
    
    private static void SeedData(EntityTypeBuilder<TaskModel> entity)
    {
        // Base date for all tasks
        var baseDate = new DateTime(2024, 1, 1);
        
        entity.HasData(
            new TaskModel
            {
                Id = 1,
                Title = "Implement User Authentication",
                Description = "Add JWT authentication to the application",
                DueDate = baseDate.AddDays(7),
                IsCompleted = false,
                CreatedAt = baseDate,
            },
            new TaskModel
            {
                Id = 2,
                Title = "Database Backup Setup",
                Description = "Configure automated daily database backups",
                DueDate = baseDate.AddDays(3),
                IsCompleted = true,
                CreatedAt = baseDate,
                UpdatedAt = baseDate.AddDays(1)
            },
            new TaskModel
            {
                Id = 3,
                Title = "Code Review",
                Description = "Review pull requests for the frontend team",
                DueDate = baseDate.AddDays(1),
                IsCompleted = false,
                CreatedAt = baseDate
            },
            new TaskModel
            {
                Id = 4,
                Title = "Deploy to Production",
                Description = "Deploy the latest version to production environment",
                DueDate = baseDate.AddDays(10),
                IsCompleted = false,
                CreatedAt = baseDate
            },
            new TaskModel
            {
                Id = 5,
                Title = "Create API Documentation",
                Description = "Write Swagger documentation for the new API endpoints",
                DueDate = baseDate.AddDays(5),
                IsCompleted = false,
                CreatedAt = baseDate
            });
    }
}