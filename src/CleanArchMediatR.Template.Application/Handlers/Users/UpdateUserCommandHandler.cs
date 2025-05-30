using CleanArchMediatR.Template.Application.Commands.Users;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;

namespace CleanArchMediatR.Template.Application.Handlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService) { 
            _userService = userService; 
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(command.id) && string.IsNullOrEmpty(command.userName))
            {
                throw new DomainException("User ID or username must be provided.");
            }

            User? existingUser = null;

            if (!string.IsNullOrEmpty(command.id))
            {
                existingUser = await _userService.GetByIdAsync(command.id);
            }
            else if (!string.IsNullOrEmpty(command.userName))
            {
                existingUser = await _userService.GetByUserNameAsync(command.userName);
            }

            if (existingUser == null)
            {
                throw new DomainException("User not found.");
            }

            if (string.IsNullOrEmpty(command.password))
            {
                throw new DomainException("Password cannot be empty.");
            }

            existingUser.passwordHash = BCrypt.Net.BCrypt.HashPassword(command.password);

            return await _userService.UpdateAsync(existingUser);

        }
    }
}
