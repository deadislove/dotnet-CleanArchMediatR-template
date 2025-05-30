using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Domain.Exceptions
{
    public class InvalidUserInputException : DomainException
    {
        public InvalidUserInputException(string message, int errorCode = 400, object? details = null) : base(message, errorCode, details)
        {
        }
    }
}
