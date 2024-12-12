using TaskChallenge.Infrastructure.Models;

namespace TaskChallenge.Application;

public struct Unit
{
    public static Unit Value => default;
}
public enum ErrorName
{
    NotFound,
    ValidationError,
    DuplicateEntry,
    DatabaseError,
    Unauthorized,
    Forbidden,
    InvalidOperation,
    ConcurrencyConflict,
    ExternalServiceError,
    RateLimit
}
 
public readonly struct ErrorType
{
    public ErrorName Name { get; }
    public string? Message { get; }
    public Dictionary<string, string[]>? Messages { get; }
 
 
    public ErrorType(ErrorName name, string message)
    {
        Name = name;
        Message = message;
        Message = null;
    }
 
    public ErrorType(ErrorName name, Dictionary<string, string[]> messages)
    {
        Name = name;
        Message = null;
        Messages = messages;
    }
};
 
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ErrorType Error { get; }
 
    private Result(T value) => (IsSuccess, Value) = (true, value);
    private Result(ErrorType errorType) => (IsSuccess, Error) = (false, errorType);
 
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(ErrorType errorType) => new(errorType);
 
   
    public TResult Match<TResult>(
        Func<T, TResult> success,
        Func<ErrorType, TResult> failure) =>
        IsSuccess && Value is not null ? success(Value) : failure(Error);
}