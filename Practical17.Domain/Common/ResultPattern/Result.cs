namespace Practical17.Domain.Common.ResultPattern;

// Result Pattern is used to represent the outcome of an operation, which can be either
// a success with a value or a failure with an error message. This pattern helps to avoid
// using exceptions for control flow and provides a clear way to handle success and failure.
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }

    private Result(bool isSuccess, T? value, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
}