using TaskChallenge.Infrastructure.Models;

namespace TaskChallenge.Application.Interfaces;

public interface ITaskService
{
    Task<Result<IEnumerable<TaskModel>>> GetAllAsync(bool? completed = null);
    
    Task<Result<TaskModel>> CreateAsync(TaskModel model);
    
    Task<Result<TaskModel>> UpdateAsync(int id, TaskModel model);
    
    Task<Result<Unit>> DeleteAsync(int id);
}