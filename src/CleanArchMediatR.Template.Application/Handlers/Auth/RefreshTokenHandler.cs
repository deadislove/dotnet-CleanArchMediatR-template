using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Application.Settings;
using CleanArchMediatR.Template.Domain.Auth.interfaces;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMediatR.Template.Application.Handlers.Auth
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly Jwt _jwt;

        public RefreshTokenHandler(IOptionsSnapshot<Jwt> jwt, IJwtTokenGenerator jwtTokenGenerator) { 
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwt = jwt.Value;
        }

        public Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.RefreshKey));

            try
            {
                ClaimsPrincipal principal = handler.ValidateToken(request.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidAudience = _jwt.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                string? tokenType = principal.Claims.FirstOrDefault(c => c.Type == "token_type")?.Value;
                if (tokenType != "refresh")
                    throw new TokenInvalidException("Invalid token type");


                string userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                string userName = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;

                User user = new User
                {
                    id = Guid.Parse(userId),
                    userName = userName
                };

                string newAccessToken = _jwtTokenGenerator.GenerateToken(user);
                string newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken(user);
                return Task.FromResult(new RefreshTokenResponseDto { 
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch
            {
                throw new TokenInvalidException();
            }
        }
    }
}
