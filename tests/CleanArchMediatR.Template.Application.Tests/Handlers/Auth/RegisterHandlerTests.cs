using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Application.Handlers.Auth;
using CleanArchMediatR.Template.Domain.Auth.interfaces;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Repository;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Application.Tests.Handlers.Auth
{
    public class RegisterHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly RegisterHandler _handler;

        public RegisterHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new RegisterHandler(_userServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessMessage_WhenUserIsCreated()
        {
            // Arrange
            RegisterCommand command = new RegisterCommand
            {
                Username = "testuser",
                Password = "testpass"
            };

            _userServiceMock.Setup(s => s.AddAsync(It.IsAny<User>()))
                            .ReturnsAsync(new User { userName = "testuser" });

            // Act
            RegisterResponseDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Register Success", result.messages);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedMessage_WhenAddAsyncReturnsNull()
        {
            // Arrange
            RegisterCommand command = new RegisterCommand
            {
                Username = "testuser",
                Password = "testpass"
            };
            
            
            _userServiceMock.Setup(s => s.AddAsync(It.IsAny<User>()))
                            .ReturnsAsync((User?)null);

            // Act
            RegisterResponseDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Register Failed", result.messages);
        }

        [Fact]
        public async Task Handle_ShouldThrowDomainException_WhenUsernameOrPasswordIsMissing()
        {
            // Arrange
            RegisterCommand command = new RegisterCommand
            {
                Username = "",
                Password = ""
            };

            // Act & Assert
            DomainException ex = await Assert.ThrowsAsync<Domain.Exceptions.DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            Assert.Contains("Username and password are required", ex.Message);
        }
    }
}
