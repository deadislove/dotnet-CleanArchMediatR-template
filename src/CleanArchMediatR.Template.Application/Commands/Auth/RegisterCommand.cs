using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using MediatR;

namespace CleanArchMediatR.Template.Application.Commands.Auth
{
    public class RegisterCommand: IRequest<RegisterResponseDto>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
