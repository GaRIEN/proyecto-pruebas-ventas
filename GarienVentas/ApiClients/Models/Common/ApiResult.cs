

namespace ApiClients.Models.Common
{
    public class ApiResult<T>(bool isSuccess, T? data, string? error)
    {
        public bool IsSuccess { get; } = isSuccess;
        public T? Data { get; } = data;
        public string? Error { get; } = error;

        public static ApiResult<T> Success(T data) => new(true, data, null);
        public static ApiResult<T> Failure(string error) => new(false, default, error);
    }
}
