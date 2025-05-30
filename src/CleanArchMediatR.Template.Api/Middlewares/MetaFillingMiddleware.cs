using System.Text;
using System.Text.Json;

namespace CleanArchMediatR.Template.Api.Middlewares
{
    public class MetaFillingMiddleware
    {
        private readonly RequestDelegate _next;

        public MetaFillingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Enable buffering so the request body can be read later in the controller
            context.Request.EnableBuffering();

            if (context.Request.ContentType?.Contains("application/json") == true &&
                context.Request.Method != HttpMethods.Get &&
                context.Request.ContentLength > 0)
            {
                context.Request.Body.Position = 0;
                using StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                string body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Check if the request body contains a "meta" field
                JsonDocument json = JsonDocument.Parse(body);
                if (json.RootElement.TryGetProperty("meta", out JsonElement metaElement))
                {
                    Dictionary<string, string> meta = new Dictionary<string, string>();

                    if (!metaElement.TryGetProperty("clientIp", out _))
                        meta["clientIp"] = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    if (!metaElement.TryGetProperty("userAgent", out _))
                        meta["userAgent"] = context.Request.Headers["User-Agent"].ToString();

                    if (!metaElement.TryGetProperty("locale", out _))
                        meta["locale"] = context.Request.Headers["Accept-Language"].ToString();

                    if (!metaElement.TryGetProperty("requestId", out _))
                        meta["requestId"] = Guid.NewGuid().ToString();

                    // Store generated or default meta in HttpContext.Items for access in controller
                    context.Items["InjectedMeta"] = meta;
                }
            }

            await _next(context);
        }
    }
}
