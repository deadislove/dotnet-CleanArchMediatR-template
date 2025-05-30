using CleanArchMediatR.Template.Application.Commands.Users;
using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;
using System.Net;

namespace CleanArchMediatR.Template.Application.Handlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        public readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(command.userName) || string.IsNullOrEmpty(command.password))
                {
                    throw new InvalidUserInputException("Username and password are required.");
                }

                User user = new()
                {
                    userName = command.userName,
                    passwordHash = BCrypt.Net.BCrypt.HashPassword(command.password)
                };

                User? result = await _userService.AddAsync(user);

                UserResponseDto response = new UserResponseDto();

                if (result != null)
                {
                    response.id = result.id;
                    response.userName = result.userName;
                    response.password = result.passwordHash;
                }


                return response;
            }
            catch (Exception ex) { 
                throw new DomainException(ex.Message, (int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
