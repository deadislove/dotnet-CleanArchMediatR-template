using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Application.Commands.Users
{
    public class CreateUserCommand: IRequest<UserResponseDto>
    {
        public string id { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
