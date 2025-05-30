using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Domain.Auth.interfaces;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchMediatR.Template.Application.Handlers.Auth
{
    public class LoginHandler: IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<LoginHandler> _logger;
        //private readonly ILoggerService _logger;

        public LoginHandler(IUserService userService, IJwtTokenGenerator jwtTokenGenerator, ILogger<LoginHandler> logger) { 
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Login - {JsonConvert.SerializeObject(request)}");
                LoginResponseDto loginResponseDto = new LoginResponseDto();
                if (!string.IsNullOrEmpty(request.Username))
                {

                    User user = await _userService.GetByUserNameAsync(request.Username);
                    if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.passwordHash))
                    {
                        throw new UnauthorizedAccessException("Invalid credentials");
                    }

                    string jwt = _jwtTokenGenerator.GenerateToken(user);
                    string refreshJwt = _jwtTokenGenerator.GenerateRefreshToken(user);

                    loginResponseDto.AccessToken = jwt;
                    loginResponseDto.RefreshToken = refreshJwt;
                }

                return loginResponseDto;
            }
            catch (Exception ex) {
                throw new DomainException(ex.Message, (int)HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
