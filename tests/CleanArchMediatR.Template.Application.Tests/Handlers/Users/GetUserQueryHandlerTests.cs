using CleanArchMediatR.Template.Application.Handlers.Users;
using CleanArchMediatR.Template.Application.Queries.Users;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Exceptions;
using CleanArchMediatR.Template.Domain.Interfaces.Services;
using Moq;

namespace CleanArchMediatR.Template.Application.Tests.Handlers.Users
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly GetUserQueryHandler _handler;

        public GetUserQueryHandlerTests()
        {
            _userService = new Mock<IUserService>();
            _handler = new GetUserQueryHandler(_userService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                id = userId,
                userName = "user1",
                passwordHash = "hashed-password"
            };

            _userService.Setup(s => s.GetByIdAsync(userId.ToString()))
                        .ReturnsAsync(user);

            var query = new GetUserQuery(userId.ToString());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.id);
            Assert.Equal("user1", result.userName);
            Assert.Equal("************", result.password);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyUserResponse_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            _userService.Setup(s => s.GetByIdAsync(userId)).ReturnsAsync((User?)null);

            var query = new GetUserQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.userName);
            Assert.Equal("************", result.password);
        }

        [Fact]
        public async Task Handle_ShouldThrowDomainException_WhenServiceThrows()
        {
            var userId = Guid.NewGuid().ToString();
            _userService.Setup(s => s.GetByIdAsync(userId)).ThrowsAsync(new Exception("something went wrong"));

            var query = new GetUserQuery(userId);

            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
