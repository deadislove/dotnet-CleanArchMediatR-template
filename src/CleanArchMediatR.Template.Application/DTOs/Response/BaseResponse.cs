namespace CleanArchMediatR.Template.Application.DTOs.Response
{
    public class BaseResponse<T>
    {
        public int code {  get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public object? Details { get; set; }
    }
}
