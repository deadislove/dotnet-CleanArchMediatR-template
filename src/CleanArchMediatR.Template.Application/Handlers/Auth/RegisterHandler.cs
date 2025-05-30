using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;
using System.Net;

namespace CleanArchMediatR.Template.Application.Handlers.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUserService _userService;

        public RegisterHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<RegisterResponseDto> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Username) || string.IsNullOrEmpty(command.Password))
                {
                    throw new InvalidUserInputException("Username and password are required.");
                }

                User user = new()
                {
                    userName = command.Username,
                    passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
                };

                User? result = await _userService.AddAsync(user);
                RegisterResponseDto registerResponseDto = new();

                if (result != null)
                {
                    registerResponseDto.messages = "Register Success";
                }
                else
                {
                    registerResponseDto.messages = "Register Failed";
                }

                return registerResponseDto;
            }
            catch (Exception ex) { 
                throw new DomainException(ex.Message, (int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
