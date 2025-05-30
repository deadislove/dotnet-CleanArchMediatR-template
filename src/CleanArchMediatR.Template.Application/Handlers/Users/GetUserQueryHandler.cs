using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using CleanArchMediatR.Template.Application.Queries.Users;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;
using System.Net;

namespace CleanArchMediatR.Template.Application.Handlers.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponseDto>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await this._userService.GetByIdAsync(request.UserId);
                UserResponseDto response = new UserResponseDto();
                if (user != null)
                {
                    response.id = user.id;
                    response.userName = user.userName;
                    response.password = user.passwordHash;
                }

                return response;
            }
            catch (Exception ex) {
                throw new DomainException(ex.Message, (int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
