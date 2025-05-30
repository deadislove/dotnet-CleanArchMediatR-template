using System.Net;

namespace CleanArchMediatR.Template.Domain.Exceptions
{
    public class TokenInvalidException: DomainException
    {
        public TokenInvalidException(string? message = "Token is invalid or expired")
            : base(message ?? "Token is invalid or expired", (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
