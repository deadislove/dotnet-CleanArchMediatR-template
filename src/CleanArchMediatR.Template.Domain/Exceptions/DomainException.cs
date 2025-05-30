using System.Net;

namespace CleanArchMediatR.Template.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public int ErrorCode { get; }
        public object? Details { get; }

        public DomainException(string message, int errorCode = (int)HttpStatusCode.BadRequest, object? details = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }

}
