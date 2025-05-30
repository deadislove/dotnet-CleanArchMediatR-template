using CleanArchMediatR.Template.Application.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMediatR.Template.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected void InjectMetaIfMissing<T>(BaseRequest<T> request)
        {
            if (request.Meta == null)
                request.Meta = new RequestMeta();

            if (HttpContext.Items.TryGetValue("InjectedMeta", out object? rawMeta) && rawMeta is Dictionary<string, string> meta)
            {
                request.Meta.RequestId ??= meta.GetValueOrDefault("requestId");
                request.Meta.ClientIp ??= meta.GetValueOrDefault("clientIp");
                request.Meta.UserAgent ??= meta.GetValueOrDefault("userAgent");
                request.Meta.Locale ??= meta.GetValueOrDefault("locale");
            }
        }
    }
}
