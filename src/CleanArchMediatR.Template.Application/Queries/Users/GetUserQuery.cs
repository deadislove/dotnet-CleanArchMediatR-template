using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Application.Queries.Users
{
    public record GetUserQuery(string UserId) : IRequest<UserResponseDto>;
}
