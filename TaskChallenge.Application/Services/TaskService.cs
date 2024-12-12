using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskChallenge.Application.Interfaces;
using TaskChallenge.Infrastructure;
using TaskChallenge.Infrastructure.Models;

namespace TaskChallenge.Application.Services;

public class TaskService(TaskDbContext taskDbContext, IValidator<TaskModel> validator) : ITaskService
{
    private readonly TaskDbContext _taskDbContext = taskDbContext;
    private readonly IValidator<TaskModel> _validator = validator;
    
    public async Task<Result<IEnumerable<TaskModel>>> GetAllAsync(bool? completed = null)
    {
        try
        {
            var query = _taskDbContext.Tasks.AsQueryable();

            if (completed.HasValue)
            {
                query = query.Where(t => t.IsCompleted == completed.Value);
            }
            var tasks = await query.ToListAsync();
            return Result<IEnumerable<TaskModel>>.Success(tasks);
        }
        catch (Exception e)
        {
            return DbError<IEnumerable<TaskModel>>(e.Message);
        }
    }

    public async Task<Result<TaskModel>> CreateAsync(TaskModel taskModel)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(taskModel);
            if (!validationResult.IsValid)
            {
                return Result<TaskModel>.Failure(new ErrorType(
                    ErrorName.ValidationError,
                    validationResult.Errors
                        .GroupBy(err => err.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        )
                ));
            }
            
            if (await _taskDbContext.Tasks.AnyAsync(t => t.Title == taskModel.Title))
                return Result<TaskModel>.Failure(new ErrorType(
                    ErrorName.DuplicateEntry,
            $"Product with ID {taskModel.Id} already exists"));
            
            var task = _taskDbContext.Tasks.Add(taskModel);
            await _taskDbContext.SaveChangesAsync();
            return Result<TaskModel>.Success(task.Entity);
        }
        catch (Exception e)
        {
            return DbError<TaskModel>(e.Message);
        }
    }

    public async Task<Result<TaskModel>> UpdateAsync(int id, TaskModel taskModel)
    {
        try
        {
            // INFO: validator logic appears twice, the next time should be extracted to a helper method
            var validationResult = await _validator.ValidateAsync(taskModel);
            if (!validationResult.IsValid)
            {
                return Result<TaskModel>.Failure(new ErrorType(
                    ErrorName.ValidationError,
                    validationResult.Errors
                        .GroupBy(err => err.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        )
                ));
            }
            
            var existingTask = await _taskDbContext.Tasks.FindAsync(id);
            
            if (existingTask == null)
                return Result<TaskModel>.Failure(new ErrorType(
                    ErrorName.NotFound, 
                    $"Task with ID: {id} does not exist"));

            taskModel.Id = id;
            
            _taskDbContext.Entry(existingTask).CurrentValues.SetValues(taskModel);
            await _taskDbContext.SaveChangesAsync();
            return Result<TaskModel>.Success(existingTask);
        }
        catch (Exception e)
        {
            return DbError<TaskModel>(e.Message);
        }
    }

    public async Task<Result<Unit>> DeleteAsync(int id)
    {
        try
        {
            var product = await _taskDbContext.Tasks.FindAsync(id);
            if (product == null)
                return Result<Unit>.Failure(new ErrorType(
                    ErrorName.NotFound, 
            $"Task with ID: {id} does not exist"));

            _taskDbContext.Remove(product);
            await _taskDbContext.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return DbError<Unit>(e.Message);
        }
    }
    
    // TODO: move to common
    private Result<T> DbError<T>(string errorMessage)
        => Result<T>.Failure(new ErrorType(ErrorName.DatabaseError, errorMessage));
}