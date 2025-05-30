using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Application.Handlers.Auth;
using CleanArchMediatR.Template.Domain.Auth.interfaces;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchMediatR.Template.Application.Tests.Handlers.Auth
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly Mock<ILogger<LoginHandler>> _mockLogger;
        private readonly LoginHandler _handler;

        public LoginHandlerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _mockLogger = new Mock<ILogger<LoginHandler>>();

            _handler = new LoginHandler(
                _mockUserService.Object,
                _mockJwtTokenGenerator.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsTokens()
        {
            // Arrange
            LoginCommand command = new LoginCommand { Username = "testuser", Password = "testpass" };
            User user = new User { userName = "testuser", passwordHash = BCrypt.Net.BCrypt.HashPassword("testpass") };

            _mockUserService.Setup(x => x.GetByUserNameAsync("testuser"))
                            .ReturnsAsync(user);
            _mockJwtTokenGenerator.Setup(x => x.GenerateToken(user))
                                  .Returns("access_token");
            _mockJwtTokenGenerator.Setup(x => x.GenerateRefreshToken(user))
                                  .Returns("refresh_token");

            // Act
            LoginResponseDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("access_token", result.AccessToken);
            Assert.Equal("refresh_token", result.RefreshToken);
        }

        [Fact]
        public async Task Handle_InvalidPassword_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            LoginCommand command = new LoginCommand { Username = "testuser", Password = "wrongpass" };
            User user = new User { userName = "testuser", passwordHash = BCrypt.Net.BCrypt.HashPassword("correctpass") };

            _mockUserService.Setup(x => x.GetByUserNameAsync("testuser"))
                            .ReturnsAsync(user);

            // Act & Assert
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Invalid credentials", exception.Message);
        }

        [Fact]
        public async Task Handle_EmptyUsername_ReturnsEmptyResponse()
        {
            // Arrange
            LoginCommand command = new LoginCommand { Username = "", Password = "any" };

            // Act
            LoginResponseDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Empty(result.AccessToken);
            Assert.Empty(result.RefreshToken);
        }
    }
}
