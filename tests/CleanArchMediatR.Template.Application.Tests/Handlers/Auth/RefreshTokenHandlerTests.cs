using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Application.Handlers.Auth;
using CleanArchMediatR.Template.Application.Settings;
using CleanArchMediatR.Template.Domain.Auth.interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMediatR.Template.Application.Tests.Handlers.Auth
{
    public class RefreshTokenHandlerTests
    {
        private readonly Mock<IOptionsSnapshot<Jwt>> _mockJwtOptions;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly RefreshTokenHandler _handler;

        public RefreshTokenHandlerTests()
        {
            _mockJwtOptions = new Mock<IOptionsSnapshot<Jwt>>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

            var jwtSettings = new Jwt
            {
                RefreshKey = "super-secret-refresh-key-1234567890",
                Issuer = "TestIssuer",
                Audience = "TestAudience"
            };

            _mockJwtOptions.Setup(o => o.Value).Returns(jwtSettings);

            _handler = new RefreshTokenHandler(_mockJwtOptions.Object, _mockJwtTokenGenerator.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNewToken_WhenValidRefreshToken()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            string username = "testuser";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim("token_type", "refresh")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-refresh-key-1234567890"));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TestIssuer",
                audience: "TestAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds
            );

            string refreshToken = new JwtSecurityTokenHandler().WriteToken(token);

            _mockJwtTokenGenerator.Setup(g => g.GenerateToken(It.IsAny<Domain.Entities.User>())).Returns("new-access-token");
            _mockJwtTokenGenerator.Setup(g => g.GenerateRefreshToken(It.IsAny<Domain.Entities.User>())).Returns("new-refresh-token");

            RefreshTokenCommand command = new RefreshTokenCommand(refreshToken);

            // Act
            RefreshTokenResponseDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("new-access-token", result.AccessToken);
            Assert.Equal("new-refresh-token", result.RefreshToken);
        }
    }
}
