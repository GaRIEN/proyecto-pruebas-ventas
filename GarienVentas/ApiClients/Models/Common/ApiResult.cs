

namespace ApiClients.Models.Common
{
    public class ApiResult<T>(bool IsSuccess, T? Data, string? Error)
    {
        public static ApiResult<T> Success(T data) => new(true, data, null);
        public static ApiResult<T> Failure(string error) => new(false, default, error);
    }
}
