using System.Diagnostics.CodeAnalysis;

namespace Domain.Abstractions;
public class Result
{
    protected internal Result(bool isSuccess, Error error, string message)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidCastException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidCastException();
        }

        IsSuccess = isSuccess;
        Error = error;
        Message = message;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; } = string.Empty;

    public Error Error { get; }

    public static Result Success(string message) => new(true, Error.None, message);

    public static Result Failure(Error error, string message) => new(false, error, message);

    public static Result<T> Success<T>(T value, string message) => new(value, true, Error.None, message);

    public static Result<T> Failure<T>(Error error) => new(default!, false, error, string.Empty);

    public static Result<T> Create<T>(T? value, string message) =>
        value is not null ? Success(value, message) : Failure<T>(Error.NullValue);
}

public class Result<T> : Result
{
    private readonly T? _value;
    private static string _message = string.Empty;
    protected internal Result(T value, bool isSuccess, Error error, string message) : base(isSuccess, error, message)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed");

    public static implicit operator Result<T>(T? value) => Create(value, _message);
}