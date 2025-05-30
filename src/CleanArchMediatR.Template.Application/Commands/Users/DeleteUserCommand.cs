using MediatR;

namespace CleanArchMediatR.Template.Application.Commands.Users
{
    public record DeleteUserCommand(Guid UserId) : IRequest<bool>;
}
