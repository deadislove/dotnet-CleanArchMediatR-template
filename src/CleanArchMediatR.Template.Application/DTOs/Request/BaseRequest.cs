using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Application.DTOs.Request
{
    public class BaseRequest<T>
    {
        public T Payload { get; set; } = default!;
        public RequestMeta Meta { get; set; } = new();
    }

    public class RequestMeta
    {
        public string? RequestId { get; set; }
        public string? ClientIp { get; set; }
        public string? UserAgent { get; set; }
        public string? Locale { get; set; }
    }
}
