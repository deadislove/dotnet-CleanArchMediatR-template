using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using CleanArchMediatR.Template.Application.Queries.Users;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;
using System.Net;

namespace CleanArchMediatR.Template.Application.Handlers.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {
        private readonly IUserService _userService;
        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                User[] users = await this._userService.GetAllAsync();
                List<UserResponseDto> response = new List<UserResponseDto>();

                if (users.Length > 0)
                {
                    foreach (User user in users)
                    {
                        response.Add(new UserResponseDto
                        {
                            id = user.id,
                            userName = user.userName,
                            password = user.passwordHash
                        });
                    }

                    return response;
                }

                return response;
            }
            catch (Exception ex) {
                throw new DomainException(ex.Message, (int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
