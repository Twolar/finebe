using AspNetCoreHero.Results;

namespace finebe.webapi.Src.Models.Generic;

public class ApiResponse<T> : Result<T> where T : class
{
    public List<string> Errors { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(T data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
        Errors = null;
    }

    public ApiResponse(string message)
        : this(new List<string> { message })
    {
    }

    public ApiResponse(List<string> errors)
    {
        Succeeded = false;
        Message = "Request Failed";
        Data = default;
        Errors = errors;
    }

    public new static ApiResponse<T> Fail()
    {
        return new ApiResponse<T>
        {
            Succeeded = false,
            Errors = new List<string> { "Request Failed" }
        };
    }

    public new static ApiResponse<T> Fail(string error)
    {
        return Fail(new List<string> { error });
    }

    public static ApiResponse<T> Fail(List<string> errors)
    {
        return new ApiResponse<T>
        {
            Succeeded = false,
            Message = "Request Failed",
            Errors = errors
        };
    }

    public new static Task<ApiResponse<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public new static Task<ApiResponse<T>> FailAsync(string error)
    {
        return Task.FromResult(Fail(new List<string> { error }));
    }

    public static Task<ApiResponse<T>> FailAsync(List<string> errors)
    {
        return Task.FromResult(Fail(errors));
    }

    public new static ApiResponse<T> Success()
    {
        return new ApiResponse<T>
        {
            Succeeded = true
        };
    }

    public new static ApiResponse<T> Success(string message)
    {
        return new ApiResponse<T>
        {
            Succeeded = true,
            Message = message
        };
    }

    public new static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T>
        {
            Succeeded = true,
            Data = data
        };
    }

    public new static ApiResponse<T> Success(T data, string message)
    {
        return new ApiResponse<T>
        {
            Succeeded = true,
            Data = data,
            Message = message
        };
    }

    public new static Task<ApiResponse<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<ApiResponse<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public new static Task<ApiResponse<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public new static Task<ApiResponse<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }
}
