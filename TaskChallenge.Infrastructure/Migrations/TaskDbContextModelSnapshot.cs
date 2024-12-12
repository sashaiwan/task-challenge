﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskChallenge.Infrastructure;

#nullable disable

namespace TaskChallenge.Infrastructure.Migrations
{
    [DbContext(typeof(TaskDbContext))]
    partial class TaskDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TaskChallenge.Infrastructure.Models.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Add JWT authentication to the application",
                            DueDate = new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = false,
                            Title = "Implement User Authentication"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Configure automated daily database backups",
                            DueDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = true,
                            Title = "Database Backup Setup",
                            UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Review pull requests for the frontend team",
                            DueDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = false,
                            Title = "Code Review"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Deploy the latest version to production environment",
                            DueDate = new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = false,
                            Title = "Deploy to Production"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Write Swagger documentation for the new API endpoints",
                            DueDate = new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = false,
                            Title = "Create API Documentation"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
