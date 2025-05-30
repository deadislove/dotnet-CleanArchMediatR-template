using CleanArchMediatR.Template.Application.Commands.Users;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;

namespace CleanArchMediatR.Template.Application.Handlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (command.UserId == Guid.Empty)
            {
                throw new DomainException("UserId cannot be empty.");
            }

            return await this._userService.DeleteAsync(command.UserId.ToString());            
        }
    }
}
