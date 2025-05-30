using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using MediatR;

namespace CleanArchMediatR.Template.Application.Commands.Auth
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponseDto>;

}
