namespace TestAssignmentApi.Utils;

public class Result<T>
{
    private Result(bool isSuccess, Error error, T value)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public T Value { get; set; }
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result<T> Success(T value) => new Result<T>(true, Error.None, value);

    public static Result<T> Failure(Error error) => new Result<T>(false, error, default!);
}
