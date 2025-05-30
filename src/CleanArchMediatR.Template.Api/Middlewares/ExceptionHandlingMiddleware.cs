using CleanArchMediatR.Template.Application.DTOs.Response;
using CleanArchMediatR.Template.Application.Helpers;
using CleanArchMediatR.Template.Domain.Exceptions;
using Newtonsoft.Json;
using Serilog.Formatting.Json;

namespace CleanArchMediatR.Template.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Domain error occurred");
                
                BaseResponse<object> errorResponse = ResponseHelper.Fail<object>(ex.Message, (int)ex.ErrorCode);
                context.Response.StatusCode = (int)ex.ErrorCode;

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error occurred");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal Server Error"
                });
            }
        }
    }
}
