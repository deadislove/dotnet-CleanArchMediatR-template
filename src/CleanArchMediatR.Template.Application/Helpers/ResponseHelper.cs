using CleanArchMediatR.Template.Application.DTOs.Response;

namespace CleanArchMediatR.Template.Application.Helpers
{
    public static class ResponseHelper
    {
        public static BaseResponse<T> Success<T>(T data, int code = 200)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = data,
                Error = null
            };
        }

        public static BaseResponse<T> Fail<T>(string error, int code = 400, object? details = null)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = default,
                Error = error,
                Details = details
            };
        }

        public static BaseResponse<T> FromException<T>(Exception ex, int code = 500)
        {
            return new BaseResponse<T>
            {
                code = code,
                Data = default,
                Error = ex.Message
            };
        }
    }
}
